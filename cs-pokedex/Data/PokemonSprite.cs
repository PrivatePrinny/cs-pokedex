namespace cs_pokedex.Data
{
	public class PokemonSprite
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string Version { get; set; } = string.Empty;
		public string SpriteType { get; set; } = string.Empty;
		public string SpriteUrl { get; set; } = string.Empty;
	}
}
