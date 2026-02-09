namespace cs_pokedex.Data
{
	public class PokemonForm
	{
		public int Id { get; set; }
		public int PokemonId { get; set; }
		public string FormName { get; set; } = string.Empty;
		public bool isDefault { get; set; } = false;
		public bool isMega { get; set; } = false;
	}
}
