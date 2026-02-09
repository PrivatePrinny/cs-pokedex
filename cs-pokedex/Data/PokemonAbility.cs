namespace cs_pokedex.Data
{
	public class PokemonAbility
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string ShortDescription { get; set; } = string.Empty;
	}
}
