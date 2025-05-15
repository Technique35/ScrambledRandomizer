using System.Globalization;

namespace ScrambledRandomizer
{
    public struct Pokemon
    {
        public string Species;
        public int Level;
        public List<string> Types;
        public Pokemon(string species, int level, List<string> types)
        {
            Species = species;
            Level = level;
            Types = types;
        }
    }

    public struct Box
    {
        public string Name;
        public int Level;
        public List<Pokemon> Pokemons;
        public Box(string name, int level, List<Pokemon> pokemons)
        {
            Name = name;
            Level = level;
            Pokemons = pokemons;
        }
    }

    public struct Boss
    {
        public string Name;
        public int Level;
        public Boss(string name, int level)
        {
            Name = name;
            Level = level;
        }
    }

    public class Program
    {
        static void Main()
        {
            string dataDirectory = Directory.GetCurrentDirectory() + "\\data";

            StreamWriter sw;
            int seed;
            var rand = new Random(DailySeed()); // Initialize daily randomization and create seed file

            List<Box> boxes = [];
            List<Box> starterBoxes = [];
            List<int> levels = [];
            ReadBoxes();

            List<Pokemon> pokemons = [];
            int pokemonMaxLength = 0;
            CompileListOfPokemon();

            List<Boss> bosses = [];
            int bossMaxLength = 0;
            ReadPath();

            GenerateSeed();

            sw.Close();

            Console.WriteLine($"Done! Please check \\seeds\\{seed}.txt for your seed.");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

            int DailySeed()
            {
                var rand = new Random();

                Console.WriteLine("Please enter a seed. Leave blank for a random seed. Type \"daily\" for today's seed.");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "daily":
                    case "Daily":
                        DateTime dateTime = DateTime.Now;
                        DateTime date = dateTime.Date;
                        seed = Int32.Parse(date.ToString("yyyyMMdd"));
                        break;
                    case "":
                        seed = rand.Next();
                        break;
                    default:
                        try
                        {
                            seed = Convert.ToInt32(input);
                        }
                        catch
                        {
                            Console.WriteLine("Invalid seed. Using random seed value instead.");
                            seed = rand.Next();
                        }
                        break;
                }
                Console.WriteLine($"Seed: {seed}");

                // seed = 77777777;  // Seed Override

                sw = new StreamWriter(Directory.GetCurrentDirectory() + $"\\seeds\\{seed}.txt", false); // Create seed file

                sw.WriteLine($"Seed: {seed}");
                sw.WriteLine();

                return seed;
            }

            void ReadBoxes()
            {
                string line;
                string[] parts;

                Console.WriteLine("Reading box data...");

                try
                {
                    StreamReader sr = new StreamReader(dataDirectory + "\\boxes.txt"); // Open list of boxes
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        parts = line.Split(","); // Separate box name and level

                        List<Pokemon> pokemons = [];
                        string line2;
                        List<string> parts2;
                        List<string> types;

                        try
                        {
                            StreamReader sr2 = new StreamReader(dataDirectory + $"\\boxes\\{parts[0]}.txt"); // Open box data
                            line2 = sr2.ReadLine();

                            List<string> genders = ["Male", "Female"];
                            List<string> taurosForms = ["Aqua", "Blaze", "Combat"];
                            List<string> taurosTypes = ["Water", "Fire", "Dark"];
                            List<string> miniorForms = ["Pink", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet"];
                            List<string> miniorTypes = ["Fairy", "Fire", "Electric", "Grass", "Ice", "Water", "Dragon"];
                            int index;

                            while (line2 != null)
                            {
                                parts2 = line2.Split(',').ToList(); // Separate species name and types
                                switch (parts2[0])
                                {
                                    case "Espurr":
                                    case "Lechonk":
                                    case "Indeedee":
                                    case "Basculin-Hisui":
                                        index = rand.Next(genders.Count);
                                        parts2[0] = $"{parts2[0]}-{genders[index]}";
                                        break;
                                    case "Tauros-Paldea":
                                        index = rand.Next(taurosForms.Count);
                                        parts2[0] = $"{parts2[0]}-{taurosForms[index]}";
                                        parts2.Add(taurosTypes[index]);
                                        break;
                                    case "Minior":
                                        index = rand.Next(miniorForms.Count);
                                        parts2[0] = $"{parts2[0]}-{miniorForms[index]}";
                                        parts2.Add(miniorTypes[index]);
                                        break;
                                }
                                types = new List<string>(parts2);
                                types.RemoveAt(0);
                                pokemons.Add(new Pokemon(parts2[0], Int32.Parse(parts[1]), types)); // Add each Pokemon to the list
                                line2 = sr2.ReadLine();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                        }

                        boxes.Add(new Box(parts[0], Int32.Parse(parts[1]), pokemons)); // Add each box to the list
                        line = sr.ReadLine();
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }

                foreach (var i in boxes)
                {
                    if (!levels.Contains(i.Level)) // Add each unique level threshold to a list
                    {
                        levels.Add(i.Level);
                    }

                    if (i.Level == 0) // Add starter boxes to their own list
                    {
                        starterBoxes.Add(i);
                    }
                }
                levels.Add(999);

            }

            void CompileListOfPokemon()
            {
                Console.WriteLine("Compiling list of Pokémon...");

                foreach (var i in boxes)
                {
                    foreach (var pokemon in i.Pokemons)
                    {
                        pokemons.Add(pokemon);
                    }
                }

                foreach (var pokemon in pokemons) // Find the length of the longest Pokemon name
                {
                    if (pokemon.Species.Length > pokemonMaxLength)
                    {
                        pokemonMaxLength = pokemon.Species.Length;
                    }
                }
            }

            void ReadPath()
            {
                Console.WriteLine("Selecting path...");

                string line;
                string[] parts;
                List<string> paths = new List<string>();

                try
                {
                    StreamReader sr = new StreamReader(dataDirectory + "\\paths.txt");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        paths.Add(line);
                        line = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
                int index = rand.Next(paths.Count);
                string path = paths[index];

                TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
                sw.WriteLine($"Path: {myTI.ToTitleCase(path)}");
                sw.WriteLine();

                try
                {
                    StreamReader sr = new StreamReader(dataDirectory + $"\\paths\\{path}.txt");
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        parts = line.Split(",");
                        bosses.Add(new Boss(parts[0], Int32.Parse(parts[1])));
                        line = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }

                foreach (Boss boss in bosses)
                {
                    if (boss.Name.Length > bossMaxLength)
                    {
                        bossMaxLength = boss.Name.Length;
                    }
                }
            }

            void GenerateSeed()
            {
                List<string> types = [
                    "Normal",
                    "Fire",
                    "Water",
                    "Electric",
                    "Grass",
                    "Ice",
                    "Fighting",
                    "Poison",
                    "Ground",
                    "Flying",
                    "Psychic",
                    "Bug",
                    "Rock",
                    "Ghost",
                    "Dragon",
                    "Dark",
                    "Steel",
                    "Fairy"
                ];

                List<string> natures = [
                    "Adamant",
                    "Bashful",
                    "Bold",
                    "Brave",
                    "Calm",
                    "Careful",
                    "Docile",
                    "Gentle",
                    "Hardy",
                    "Hasty",
                    "Impish",
                    "Jolly",
                    "Lax",
                    "Lonely",
                    "Mild",
                    "Modest",
                    "Naive",
                    "Naughty",
                    "Quiet",
                    "Quirky",
                    "Rash",
                    "Relaxed",
                    "Sassy",
                    "Serious",
                    "Timid"
                ];

                Console.WriteLine("Generating starters...");

                int typesCount = types.Count;
                int naturesCount = natures.Count;

                List<Pokemon> starters = new List<Pokemon>();

                foreach (Pokemon pokemon in pokemons)
                {
                    if (pokemon.Level == 0)
                    {
                        starters.Add(pokemon);
                    }
                }

                levels.RemoveAt(0);
                pokemons = pokemons.Except(starters).ToList();

                int index;

                for (int i = 0; i < 6; i++) // Generate Starters
                {
                    index = rand.Next(starterBoxes.Count);
                    Pokemon choice = starterBoxes[index].Pokemons[rand.Next(starterBoxes[index].Pokemons.Count)];

                    List<string> teraTypes = new List<string>(types);
                    teraTypes.Except(choice.Types);
                    int teraTypesCount = teraTypes.Count;

                    starters.Remove(choice);
                    sw.WriteLine($"Starter {i + 1}: {choice.Species}{String.Concat(Enumerable.Repeat(" ", pokemonMaxLength - choice.Species.Length + 1))}" +
                        $"({natures[rand.Next(naturesCount)]}, Tera {teraTypes[rand.Next(teraTypesCount)]})");

                    starterBoxes.RemoveAt(index);
                }

                sw.WriteLine();

                Console.WriteLine("Generating boss pickups...");

                foreach (Boss boss in bosses)
                {
                    if (boss.Level >= levels[0]) // Check for New Pokemon at this level
                    {
                        foreach (Pokemon pokemon in pokemons) // And add them to the Starters list
                        {
                            if (pokemon.Level == levels[0])
                            {
                                starters.Add(pokemon);
                            }
                        }

                        levels.RemoveAt(0);
                        pokemons = pokemons.Except(starters).ToList();

                    }

                    index = rand.Next(starters.Count);
                    Pokemon choice = starters[index];

                    List<string> teraTypes = new List<string>(types);
                    teraTypes.Except(choice.Types);
                    int teraTypesCount = teraTypes.Count;

                    sw.WriteLine($"After {boss.Name}:{String.Concat(Enumerable.Repeat(" ", bossMaxLength - boss.Name.Length + 1))}" +
                        $"{choice.Species}{String.Concat(Enumerable.Repeat(" ", pokemonMaxLength - choice.Species.Length + 1))}" +
                        $"({natures[rand.Next(naturesCount)]}, Tera {teraTypes[rand.Next(teraTypesCount)]})");

                    if (boss.Level < 30) // If boss is below level 30, generate a second Pokemon
                    {
                        index = rand.Next(starters.Count);
                        Pokemon choice2 = starters[index];

                        List<string> teraTypes2 = new List<string>(types);
                        teraTypes2.Except(choice2.Types);
                        int teraTypesCount2 = teraTypes2.Count;

                        sw.WriteLine($"{String.Concat(Enumerable.Repeat(" ", bossMaxLength + 8))}" +
                        $"{choice2.Species}{String.Concat(Enumerable.Repeat(" ", pokemonMaxLength - choice2.Species.Length + 1))}" +
                        $"({natures[rand.Next(naturesCount)]}, Tera {teraTypes2[rand.Next(teraTypesCount)]})");
                    }

                    starters.Remove(choice);

                    sw.WriteLine();
                }

                Console.WriteLine("Generating Tera Type rerolls...");

                sw.WriteLine("Tera Rerolls:");
                for (int i = 0; i < 20; i++)
                {
                    sw.WriteLine($"  {types[rand.Next(typesCount)]}");
                }

            }

        }
    }
}