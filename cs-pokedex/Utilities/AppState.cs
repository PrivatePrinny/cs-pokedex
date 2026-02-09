using cs_pokedex.Data;

namespace cs_pokedex.Utilities
{
	public class AppState : Observer
	{
		private Pokemon? selectedPokemon = null;
		private int selectedNationalDexNumber = 0;
		private String selectedRegion = "National";
		public String SelectedRegion
		{
			get => selectedRegion;
			set
			{
				selectedRegion = value;
				base.BroadcastStateChange();
			}
		}
		public Pokemon? SelectedPokemon
		{
			get => selectedPokemon;
			set
			{
				selectedPokemon = value;
				base.BroadcastStateChange();
			}
		}
		public int SelectedNationalDexNumber
		{
			get => selectedNationalDexNumber;
			set
			{
				selectedNationalDexNumber = value;
				base.BroadcastStateChange();
			}
		}
	}
}
