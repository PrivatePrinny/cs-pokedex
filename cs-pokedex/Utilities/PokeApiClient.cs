using cs_pokedex.Data;
using cs_pokedex.Extensions;
using cs_pokedex.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace cs_pokedex.Utilities
{
	public class PokeApiClient
	{
		private static readonly HttpClient client = new HttpClient();
		private const string baseUrl = "https://pokeapi.co/api/v2/";
		private readonly IPokemonRepository _pokemonRepository;

		static JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		// Konstruktor-Injektion des Repositories
		public PokeApiClient(IPokemonRepository pokemonRepository)
		{
			_pokemonRepository = pokemonRepository;
		}

		public async Task<Pokemon?> GetPokemonByNationalDexNumberAsync(int nationalDexNumber)
		{
			Pokemon? pokemon = null;
			JsonNode? jsonNode = await GetByUrl(baseUrl + "pokemon/" + nationalDexNumber);
			List<PokemonAbility> pokemonAbilities = new List<PokemonAbility>();

			if (jsonNode != null)
			{
				pokemon = this._pokemonRepository.GetByNationalDexNumber(nationalDexNumber);

				if (pokemon == null)
				{
					pokemon = new Pokemon();
				}

				pokemon.Name = jsonNode.GetStringOrEmpty("name");
				pokemon.BaseExperience = jsonNode.GetIntOrZero("base_experience");
				pokemon.Height = jsonNode.GetIntOrZero("height");
				pokemon.Weight = jsonNode.GetIntOrZero("weight");
				pokemon.NationalDexNumber = jsonNode.GetIntOrZero("id");

				pokemon.HP = jsonNode["stats"]?[0]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.Attack = jsonNode["stats"]?[1]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.Defense = jsonNode["stats"]?[2]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.SpecialAttack = jsonNode["stats"]?[3]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.SpecialDefense = jsonNode["stats"]?[4]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.Speed = jsonNode["stats"]?[5]?["base_stat"]?.GetValue<int>() ?? 0;
				pokemon.Type1 = jsonNode["types"]?[0]?["type"]?["name"]?.ToString() ?? String.Empty;
				JsonArray? types = jsonNode["types"]?.AsArray();

				if (types?.Count > 1)
				{
					pokemon.Type2 = jsonNode["types"]?[1]?["type"]?["name"]?.ToString() ?? String.Empty;
				}

				if (pokemon.Id == 0)
				{
					await _pokemonRepository.AddAsync(pokemon);
				}

				JsonArray abilities = jsonNode["abilities"]?.AsArray() ?? new JsonArray();

				foreach (var ability in abilities)
				{
					JsonNode? abilityDetails = await GetByUrl(ability?["ability"]?["url"]?.ToString() ?? String.Empty);
					String abilityName = abilityDetails.GetStringOrEmpty("name");

					PokemonAbility? dbAbility = pokemon.Abilities.Where((tmpAbility) => tmpAbility.Name == abilityName).SingleOrDefault();

					if (dbAbility == null)
					{
						PokemonAbility pokemonAbility = new PokemonAbility();
						pokemonAbility.PokemonId = pokemon.Id;
						pokemonAbility.Name = abilityName;

						JsonArray effectEntries = abilityDetails?["effect_entries"]?.AsArray() ?? new JsonArray();

						foreach (var effectEntry in effectEntries)
						{
							if (effectEntry?["language"]?["name"]?.ToString() == "en")
							{
								pokemonAbility.Description = effectEntry.GetStringOrEmpty("effect");
								pokemonAbility.ShortDescription = effectEntry.GetStringOrEmpty("short_effect");
								break;
							}
						}

						pokemon.Abilities.Add(pokemonAbility);
					}
				}

				JsonNode? cries = jsonNode["cries"] ?? null;

				if (pokemon.Cries.Count == 0)
				{
					PokemonCry pokemoncry = new();

					pokemoncry.PokemonId = pokemon.Id;
					pokemoncry.OriginalUrl = cries.GetStringOrEmpty("legacy");
					pokemoncry.LatestUrl = cries.GetStringOrEmpty("latest");

					pokemon.Cries.Add(pokemoncry);
				}

				JsonNode? locations = await GetByUrl(jsonNode.GetStringOrEmpty("location_area_encounters"));

				foreach (var location in locations?.AsArray() ?? new JsonArray())
				{
					String locationName = location?["location_area"]?["name"]?.ToString() ?? String.Empty;
					PokemonLocation? dbLocation = pokemon.Locations.Where((tmpLocation) => tmpLocation.LocationName == locationName).SingleOrDefault();

					if (dbLocation == null)
					{
						PokemonLocation pokemonLocation = new();
						pokemonLocation.PokemonId = pokemon.Id;
						pokemonLocation.LocationName = locationName;

						pokemon.Locations.Add(pokemonLocation);
						// chance, minLevel, maxLevel, method?
					}
				}

				JsonArray moves = jsonNode["moves"]?.AsArray() ?? new JsonArray();

				foreach (var move in moves)
				{
					JsonNode? subMove = move?["move"];
					String moveName = subMove.GetStringOrEmpty("name");

					PokemonMove? dbMove = pokemon.Moves.Where((tmpMove) => tmpMove.MoveName == moveName).SingleOrDefault();

					if (dbMove == null)
					{
						PokemonMove pokemonMove = new();
						pokemonMove.PokemonId = pokemon.Id;
						pokemonMove.MoveName = moveName;

						JsonNode? moveSubElement = await GetByUrl(subMove.GetStringOrEmpty("url"));
						pokemonMove.Accuracy = moveSubElement?.GetIntOrZero("accuracy") ?? 0;
						pokemonMove.Power = moveSubElement?.GetIntOrZero("power") ?? 0;

						pokemon.Moves.Add(pokemonMove);
					}
				}

				List<PokemonSprite> pokemonSprites = new List<PokemonSprite>();
				AddSpritesFromJson(jsonNode["sprites"] ?? null, pokemonSprites, pokemon);
				pokemon.Sprites.AddRange(pokemonSprites);
				
			}

			if (pokemon != null)
			{
				await _pokemonRepository.UpdateAsync(pokemon);
			}

			return pokemon;
		}

		public static async Task<JsonNode?> GetByUrl(String url)
		{
			if (String.IsNullOrEmpty(url)) return null;

			HttpResponseMessage response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				JsonNode jsonNode = JsonNode.Parse(json)!;
				if (jsonNode != null)
				{
					return jsonNode;
				}
			}

			return null;
		}

		private static void AddSpritesFromJson(JsonNode? spritesNode, List<PokemonSprite> target, Pokemon pokemon)
		{
			if (spritesNode is not JsonObject spritesObj) return;

			foreach (var kvp in spritesObj)
			{
				var key = kvp.Key;
				var value = kvp.Value;

				target.AddRange(recursiveUrlFetch(kvp, pokemon));
			}
		}

		private static List<PokemonSprite> recursiveUrlFetch(KeyValuePair<string, JsonNode?> kvp, Pokemon pokemon)
		{
			var key = kvp.Key;
			var value = kvp.Value;
			List<PokemonSprite> list = new List<PokemonSprite>();

			if (value is JsonObject innerObj)
			{
				foreach (var innerKvp in innerObj)
				{
					list.AddRange(recursiveUrlFetch(innerKvp, pokemon));
				}
			}
			else
			{
				var url = value?.ToString() ?? string.Empty;

				if (!String.IsNullOrEmpty(url) && url != "null")
				{
					PokemonSprite? dbSprite = pokemon.Sprites.Where((sprite) => sprite.SpriteUrl == url).SingleOrDefault();

					if (dbSprite == null)
					{
						list.Add(new PokemonSprite { SpriteUrl = url, SpriteType = key, PokemonId = pokemon.Id });
					}
				}
			}

			return list;
		}
	}
}
