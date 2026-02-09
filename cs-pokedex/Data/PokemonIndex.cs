namespace cs_pokedex.Data
{
	public class PokemonIndex
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public int Generation { get; set; } = 0;
		public int RegionalDexNumber { get; set; } = 0;
		public String Region { get; set; } = string.Empty;
		public string Version { get; set; } = string.Empty;

	}
}
