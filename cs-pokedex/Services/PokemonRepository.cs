using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cs_pokedex.Data;

namespace cs_pokedex.Services
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly ApplicationDbContext _db;

        public PokemonRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<Pokemon>> GetAllAsync()
        {
            return await _db.Pokemons.AsNoTracking().ToListAsync();
        }

        public async Task<Pokemon?> GetByIdAsync(int id)
        {
            Pokemon? pokemon = await _db.Pokemons.FindAsync(id);

            if (pokemon != null)
            {
                pokemon.Abilities = await _db.PokemonAbilities.Where((a) => a.PokemonId == id).ToListAsync();
                pokemon.Cries = await _db.PokemonCries.Where((c) => c.PokemonId == id).ToListAsync();
                pokemon.Locations = await _db.PokemonLocations.Where((l) => l.PokemonId == id).ToListAsync();
                pokemon.Moves = await _db.PokemonMoves.Where((m) => m.PokemonId == id).ToListAsync();
                pokemon.Sprites = await _db.PokemonSprites.Where((s) => s.PokemonId == id).ToListAsync();
			}

            return pokemon;
		}

        public Pokemon? GetByNationalDexNumber(int nationalDexNumber)
        {
            return _db.Pokemons.Where((p) => p.NationalDexNumber == nationalDexNumber).First();
        }

        public async Task AddAsync(Pokemon pokemon)
        {
            _db.Pokemons.Add(pokemon);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Pokemon pokemon)
        {
            _db.Pokemons.Update(pokemon);

            pokemon.Abilities.ForEach(async (ability) => await this.SaveAbility(ability));
            pokemon.Cries.ForEach(async (cry) => await this.SaveCry(cry));
            pokemon.Locations.ForEach(async (location) => await this.SaveLocation(location));
            pokemon.Moves.ForEach(async (move) => await this.SaveMove(move));
            pokemon.Sprites.ForEach(async (sprite) => await this.SaveSprite(sprite));

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _db.Pokemons.FindAsync(id);
            if (entity != null)
            {
                _db.Pokemons.Remove(entity);
                await _db.SaveChangesAsync();
            }
        }

        public async Task SaveAbility(PokemonAbility ability)
        {
            if (ability.Id == 0)
            {
                _db.PokemonAbilities.Add(ability);

            }
            else
            {
                _db.PokemonAbilities.Update(ability);
            }

            await _db.SaveChangesAsync();
        }

        public async Task SaveCry(PokemonCry cry)
        {
            if (cry.Id == 0)
            {
                _db.PokemonCries.Add(cry);
            }
            else
            {
                _db.PokemonCries.Update(cry);
            }
            await _db.SaveChangesAsync();
        }
        public async Task SaveLocation(PokemonLocation location)
        {
            if (location.Id == 0)
            {
                _db.PokemonLocations.Add(location);
            }
            else
            {
                _db.PokemonLocations.Update(location);
            }
            await _db.SaveChangesAsync();
        }
        public async Task SaveMove(PokemonMove move)
        {
            if (move.Id == 0)
            {
                _db.PokemonMoves.Add(move);
            }
            else
            {
                _db.PokemonMoves.Update(move);
            }
            await _db.SaveChangesAsync();
        }
        public async Task SaveSprite(PokemonSprite sprite)
        {
            if (sprite.Id == 0)
            {
                _db.PokemonSprites.Add(sprite);
            }
            else
            {
                _db.PokemonSprites.Update(sprite);
            }
            await _db.SaveChangesAsync();
        }
    }
}
