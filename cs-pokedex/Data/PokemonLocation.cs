namespace cs_pokedex.Data
{
	public class PokemonLocation
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string LocationName { get; set; } = string.Empty;
		public string Region { get; set; } = string.Empty;
		public string Version { get; set; } = string.Empty;
	}
}
