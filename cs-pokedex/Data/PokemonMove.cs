namespace cs_pokedex.Data
{
	public class PokemonMove
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string MoveName { get; set; } = string.Empty;
		public int Accuracy { get; set; } = 0;
		public int Power { get; set; } = 0;
	}
}
