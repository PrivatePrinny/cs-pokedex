using Microsoft.EntityFrameworkCore;
using cs_pokedex.Data;

namespace cs_pokedex.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<PokemonSprite> PokemonSprites { get; set; }
        public DbSet<PokemonMove> PokemonMoves { get; set; }
        public DbSet<PokemonLocation> PokemonLocations { get; set; }
        public DbSet<PokemonIndex> PokemonIndexes { get; set; }
        public DbSet<PokemonForm> PokemonForms { get; set; }
        public DbSet<PokemonCry> PokemonCries { get; set; }
        public DbSet<PokemonAbility> PokemonAbilities { get; set; }
    }
}
