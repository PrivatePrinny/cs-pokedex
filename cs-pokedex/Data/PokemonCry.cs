namespace cs_pokedex.Data
{
	public class PokemonCry
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string LatestUrl	{ get; set; } = string.Empty;
		public string OriginalUrl { get; set; } = string.Empty;
	}
}
