namespace cs_pokedex.Utilities
{
    public class Pokedex
    {
        public static string[] Regions = ["National", "Kanto", "Johto", "Hoenn", "Sinnoh", "Unova", "Kalos", "Alola", "Galar", "Hisui", "Paldea"];
        
        private static List<string[]> nationalDex = new List<string[]>();

        public static List<string[]> GetNationalDex()
        {
            if (nationalDex.Count == 0)
            {
                LoadCsv();
            }

            return nationalDex;
        }

        public static List<string[]> LoadCsv()
        {
            if (nationalDex.Count > 0)
            {
                return nationalDex;
            }

            var lines = File.ReadAllLines("pokemon.csv");

            foreach (var line in lines)
            {
                if (lines.FirstOrDefault() == line)
                {
                    continue;
                }

                var values = line.Split(';');
                nationalDex.Add(values);
            }

            return nationalDex;
        }

        public static List<string[]> kantoDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) <= 151).ToList();
        }

        public static List<string[]> johtoDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 151 && int.Parse(p[0]) <= 251).ToList();
        }

        public static List<string[]> hoennDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 251 && int.Parse(p[0]) <= 386).ToList();
        }

        public static List<string[]> sinnohDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 386 && int.Parse(p[0]) <= 493).ToList();
        }

        public static List<string[]> unovaDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 493 && int.Parse(p[0]) <= 649).ToList();
        }

        public static List<string[]> kalosDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 649 && int.Parse(p[0]) <= 721).ToList();
        }

        public static List<string[]> alolaDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 721 && int.Parse(p[0]) <= 809).ToList();
        }

        public static List<string[]> galarDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 809 && int.Parse(p[0]) <= 898).ToList();
        }

        public static List<string[]> hisuiDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 898 && int.Parse(p[0]) <= 906).ToList();
        }
        public static List<string[]> paldeaDex()
        {
            return GetNationalDex().Where(p => int.Parse(p[0]) > 906).ToList();
        }
    }
}



