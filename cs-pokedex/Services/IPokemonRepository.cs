using System.Collections.Generic;
using System.Threading.Tasks;
using cs_pokedex.Data;

namespace cs_pokedex.Services
{
    public interface IPokemonRepository
    {
        Task<List<Pokemon>> GetAllAsync();
        Task<Pokemon?> GetByIdAsync(int id);
        Task AddAsync(Pokemon pokemon);
        Task UpdateAsync(Pokemon pokemon);
        Task DeleteAsync(int id);
        Pokemon? GetByNationalDexNumber(int nationalDexNumber);

	}
}
