using cs_pokedex.Data;
using System.Text.Json.Nodes;

namespace cs_pokedex.Data
{
	public class Pokemon
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Type1 { get; set; } = string.Empty;
		public string Type2 { get; set; } = string.Empty;
		public int BaseExperience { get; set; } = 0;
		public int Height { get; set; } = 0;
		public int Weight { get; set; } = 0;
		public int NationalDexNumber { get; set; } = 0;
		public int HP { get; set; } = 0;
		public int Attack { get; set; } = 0;
		public int Defense { get; set; } = 0;
		public int SpecialAttack { get; set; } = 0;
		public int SpecialDefense { get; set; } = 0;
		public int Speed { get; set; } = 0;

		public List<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();
		public List<PokemonCry> Cries { get; set; } = new List<PokemonCry>();
		public List<PokemonLocation> Locations { get; set; } = new List<PokemonLocation>();
		public List<PokemonMove> Moves { get; set; } = new List<PokemonMove>();
		public List<PokemonSprite> Sprites { get; set; } = new List<PokemonSprite>();
		
	}
}
