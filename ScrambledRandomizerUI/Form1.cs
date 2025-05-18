using System;
using System.ComponentModel;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace ScrambledRandomizer
{
    public partial class Form1 : Form
    {
        List<Box> boxes = new List<Box>();
        List<int> levels = new List<int>();
        List<Path> paths = new List<Path>();

        static List<string> types = [
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

        static List<string> natures = [
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

        public class Pokemon(string species, int level, List<string> types)
        {
            public string Species = species;
            public int Level = level;
            public List<string> Types = types;
            public bool Enabled = true;
            public string TeraType = "";
            public string Nature = "";

            public override string ToString() { return Species; }
        }

        public class Box(string name, int level, List<Form1.Pokemon> pokemons)
        {
            public string Name = name;
            public int Level = level;
            public List<Pokemon> Pokemons = pokemons;
            public bool Enabled = true;

            public override string ToString() { return Name; }
        }

        public class Boss(string name, int level, bool dlc)
        {
            public string Name = name;
            public int Level = level;
            public bool DLC = dlc;

            public override string ToString() { return Name; }
        }

        public class Path(string name, List<Boss> bosses)
        {
            public string Name = name;
            public List<Boss> Bosses = bosses;

            public override string ToString() { return Name; }
        }

        string dataDirectory = Directory.GetCurrentDirectory() + "\\data";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seedComboBox.SelectedIndex = 0;
            modeComboBox.SelectedIndex = 0;
            pathComboBox.SelectedIndex = 0;

            ReadBoxes();

            foreach (Box box in boxes)
            {
                boxesCheckedList.Items.Add(box.Name); // Load boxes into the checked list
            }

            for (int i = 0; i < boxes.Count; i++)
            {
                if (boxes[i].Enabled)
                {
                    boxesCheckedList.SetItemChecked(i, true);
                }
            }

            ReadPaths();

            foreach (Path path in paths)
            {
                pathComboBox.Items.Add(path);
            }

            void ReadBoxes()
            {
                string line;
                string[] parts;

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

                            while (line2 != null)
                            {
                                parts2 = line2.Split(',').ToList(); // Separate species name and types

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
                }
                levels.Add(999);
            }

            void ReadPaths()
            {
                string line;

                try
                {
                    StreamReader sr = new StreamReader(dataDirectory + "\\paths.txt"); // Open list of paths
                    line = sr.ReadLine();
                    while (line != null)
                    {
                        List<Boss> bosses = [];
                        string line2;
                        string[] parts2;

                        try
                        {
                            StreamReader sr2 = new StreamReader(dataDirectory + $"\\paths\\{line}.txt");
                            line2 = sr2.ReadLine();
                            while (line2 != null)
                            {
                                parts2 = line2.Split(',');
                                bosses.Add(new Boss(parts2[0], Convert.ToInt32(parts2[1]), Convert.ToBoolean(parts2[2])));
                                line2 = sr2.ReadLine();
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"{e.Message}");
                        }

                        paths.Add(new Path(line, bosses));
                        line = sr.ReadLine();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message}");
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void modeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (modeComboBox.SelectedIndex)
            {
                case 0:
                    doubleCheckBox.Enabled = true;
                    doubleLevelNumericUpDown.Enabled = true;

                    allowDuplicates.Enabled = true;
                    allowDuplicates.Checked = false;

                    break;
                case 1:
                    doubleCheckBox.Enabled = false;
                    doubleLevelNumericUpDown.Enabled = false;

                    allowDuplicates.Enabled = false;
                    allowDuplicates.Checked = true;

                    break;
            }
        }

        private void toolTipSeed_Popup(object sender, PopupEventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void generateTeraTypes_CheckedChanged(object sender, EventArgs e)
        {
            if (generateTeraTypes.Checked)
            {
                stabTeraTypes.Enabled = true;
            }
            else
            {
                stabTeraTypes.Enabled = false;
            }
        }

        private void loadBoxes_Click(object sender, EventArgs e)
        {
        }

        private void boxesCheckedList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pokemonCheckedList.Items.Clear();

            foreach (Pokemon pokemon in boxes[boxesCheckedList.SelectedIndex].Pokemons)
            {
                pokemonCheckedList.Items.Add(pokemon);
            }

            for (int i = 0; i < pokemonCheckedList.Items.Count; i++)
            {
                if (boxes[boxesCheckedList.SelectedIndex].Pokemons[i].Enabled)
                {
                    pokemonCheckedList.SetItemChecked(i, true);
                }
            }

            if (boxes[boxesCheckedList.SelectedIndex].Enabled)
            {
                pokemonCheckedList.Enabled = true;
            }
            else
            {
                pokemonCheckedList.Enabled = false;
            }

        }

        private void boxesCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                boxes[e.Index].Enabled = true;
            }
            else
            {
                boxes[e.Index].Enabled = false;
            }
        }

        private void pokemonCheckedList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                boxes[boxesCheckedList.SelectedIndex].Pokemons[e.Index].Enabled = true;
            }
            else
            {
                boxes[boxesCheckedList.SelectedIndex].Pokemons[e.Index].Enabled = false;
            }
        }

        private void generateSeed_click(object sender, EventArgs e)
        {

            Random rand = new Random(Convert.ToInt32(seedNumericUpDown.Value));

            Stream stream;
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "txt files(*.txt)|*.txt";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.InitialDirectory = dataDirectory + "\\seeds";
            saveFileDialog.FileName = $"{Convert.ToString(seedNumericUpDown.Value)} - {modeComboBox.SelectedItem}";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if ((stream = saveFileDialog.OpenFile()) != null)
                {
                    StreamWriter sw = new StreamWriter(stream);

                    List<Pokemon> pokemons = [];
                    int pokemonMaxLength = 0;
                    int bossMaxLength = 0;

                    List<string> genders = ["Male", "Female"];
                    List<string> taurosForms = ["Aqua", "Blaze", "Combat"];
                    List<string> taurosTypes = ["Water", "Fire", "Dark"];
                    List<string> miniorForms = ["Pink", "Orange", "Yellow", "Green", "Blue", "Indigo", "Violet"];
                    List<string> miniorTypes = ["Fairy", "Fire", "Electric", "Grass", "Ice", "Water", "Dragon"];
                    int index;

                    foreach (Box box in boxes) // Add all enabled Pokémon in enabled boxes to a list
                    {
                        if (box.Enabled)
                        {
                            foreach (Pokemon pokemon in box.Pokemons)
                            {
                                if (pokemon.Enabled)
                                {
                                    switch (pokemon.Species) // Special cases for Pokémon with random forms
                                    {
                                        case "Espurr":
                                        case "Lechonk":
                                        case "Indeedee":
                                        case "Basculin-Hisui":
                                            index = rand.Next(genders.Count);
                                            pokemon.Species = $"{pokemon.Species}-{genders[index]}";
                                            break;
                                        case "Tauros-Paldea":
                                            index = rand.Next(taurosForms.Count);
                                            pokemon.Species = $"{pokemon.Species}-{taurosForms[index]}";
                                            pokemon.Types.Add(taurosTypes[index]);
                                            break;
                                        case "Minior":
                                            index = rand.Next(miniorForms.Count);
                                            pokemon.Species = $"{pokemon.Species}-{miniorForms[index]}";
                                            pokemon.Types.Add(miniorTypes[index]);
                                            break;
                                    }
                                    pokemons.Add(pokemon);
                                }
                            }
                        }
                    }

                    foreach (Pokemon pokemon in pokemons) // Find the length of the longest Pokemon name
                    {
                        if (pokemon.Species.Length > pokemonMaxLength)
                        {
                            pokemonMaxLength = pokemon.Species.Length;
                        }
                    }

                    int typesCount = types.Count;
                    int naturesCount = natures.Count;

                    index = rand.Next(paths.Count);
                    Path path;

                    switch (pathComboBox.SelectedIndex)
                    {
                        case 0: // Random
                            path = paths[index]; break;
                        default: // Selected Path
                            path = paths[pathComboBox.SelectedIndex - 1]; break;
                    }

                    int encounterCount = 6 + path.Bosses.Count;

                    if (pokemons.Count < encounterCount && !allowDuplicates.Checked)
                    {
                        MessageBox.Show("Cannot generate seed. Try enabling more Pokémon or allowing duplicates.");
                        Application.Exit();
                    }

                    if (modeComboBox.SelectedIndex == 1) // In Unique Teams mode, combine Nemona 2 with previous boss.
                    {
                        foreach (Boss boss in path.Bosses)
                        {
                            if (boss.Name == "Nemona 2")
                            {
                                int i = path.Bosses.IndexOf(boss);
                                path.Bosses.Remove(boss);
                                path.Bosses[i - 1].Name += " and Nemona 2";
                                break;
                            }
                        }
                    }

                    foreach (Boss boss in path.Bosses)
                    {
                        if (!boss.DLC || dlcCheckBox.Checked)
                        {
                            if (boss.Name.Length > bossMaxLength)
                            {
                                bossMaxLength = boss.Name.Length;
                            }
                        }
                    }

                    List<Box> availableBoxes = new List<Box>();
                    List<Pokemon> availablePokemons = new List<Pokemon>();

                    WriteSettings();

                    AppendAvailables(0);

                    levels.RemoveAt(0);

                    switch (modeComboBox.SelectedIndex)
                    {
                        case 0: // Teamlocked Mode
                            GenerateTeamlocked();
                            break;
                        case 1: // Unique Teams Mode
                            GenerateUniqueTeams();
                            break;
                    }

                    if (generateTeraTypes.Checked)
                    {
                        GenerateTeraRerolls();
                    }

                    sw.Close();
                    stream.Close();
                    MessageBox.Show("Seed generated!");
                    Application.Exit();

                    // Functions

                    void GenerateTeamlocked()
                    {
                        List<Pokemon> starters = GenerateTeam();

                        for (int i = 0; i < 6; i++)
                        {
                            sw.WriteLine($"Starter {i + 1}: {FormatPokemon(starters[i])}");
                        }

                        sw.WriteLine();

                        foreach (Boss boss in path.Bosses) // For each boss in the chosen path:
                        {
                            if (boss == path.Bosses[path.Bosses.Count - 1]) // Check if it's the last boss.
                            {
                                break; // If it is, don't generate an encounter for it.
                            }
                            if (!boss.DLC || dlcCheckBox.Checked) // If the boss is not DLC OR DLC is enabled,
                            {
                                while (boss.Level >= levels[0]) // If the new boss is past the next level threshhold
                                {
                                    AppendAvailables(levels[0]); // Add Pokemon at that threshhold
                                    levels.RemoveAt(0); // and move to the next threshhold
                                }

                                Pokemon choice = availablePokemons[rand.Next(availablePokemons.Count)]; // Get random Pokemon
                                Pokemon teamMember = new Pokemon(choice.Species, choice.Level, choice.Types); // and make a copy of it.

                                GenerateNatureAndTera(teamMember); // Generate its Nature and Tera Type.

                                sw.WriteLine($"After {FormatBoss(boss)}{FormatPokemon(teamMember)}");

                                RemoveFromAvailable(choice);

                                if (boss.Level < doubleLevelNumericUpDown.Value) // If below level for encounter doublign
                                {
                                    choice = availablePokemons[rand.Next(availablePokemons.Count)]; // Get random Pokemon
                                    teamMember = new Pokemon(choice.Species, choice.Level, choice.Types); // and make a copy of it.

                                    GenerateNatureAndTera(teamMember);

                                    sw.WriteLine($"{new string(' ', bossMaxLength + 8)}{FormatPokemon(teamMember)}");

                                    RemoveFromAvailable(choice);
                                }

                                sw.WriteLine();

                            }
                        }
                    }

                    void GenerateUniqueTeams()
                    {
                        foreach (Boss boss in path.Bosses)
                        {
                            if (!boss.DLC || dlcCheckBox.Checked)
                            {
                                while (boss.Level >= levels[0]) // If the new boss is past the next level threshhold
                                {
                                    AppendAvailables(levels[0]); // Add Pokemon at that threshhold
                                    levels.RemoveAt(0); // and move to the next threshhold
                                }

                                List<Pokemon> team = GenerateTeam();

                                for (int i = 0; i < 6; i++)
                                {
                                    if (i == 0)
                                    {
                                        sw.WriteLine($"VS {FormatBoss(boss)}{FormatPokemon(team[i])}");
                                    }
                                    else
                                    {
                                        sw.WriteLine($"{new string(' ', bossMaxLength + 5)}{FormatPokemon(team[i])}");
                                    }
                                }

                                sw.WriteLine();
                            }
                        }
                    }

                    void WriteSettings()
                    {
                        sw.WriteLine("Pokémon Scrambled Scarlet Random Seed");
                        sw.WriteLine();
                        sw.WriteLine($"Seed: {seedNumericUpDown.Value}");
                        sw.WriteLine($"Mode: {modeComboBox.SelectedItem}");
                        if (pathComboBox.SelectedIndex == 0)
                        {
                            sw.WriteLine($"Path: Random ({path})");
                        }
                        else
                        {
                            sw.WriteLine($"Path: {path}");
                        } 
                        sw.WriteLine();
                        if (modeComboBox.SelectedIndex != 1)
                        {
                            sw.WriteLine($"Allow Duplicates: {allowDuplicates.Checked}");
                        }     
                        sw.WriteLine($"Diverse Teams: {diverseTeams.Checked}");
                        sw.WriteLine($"Generate Natures: {generateNatures.Checked}");
                        sw.WriteLine($"Generate Tera Types: {generateTeraTypes.Checked}");
                        if (generateTeraTypes.Checked)
                        {
                            sw.WriteLine($"Allow STAB Tera Types: {stabTeraTypes.Checked}");
                        }
                        sw.WriteLine();

                        List<Box> disabledBoxes = new List<Box>();
                        List<Pokemon> disabledPokemon = new List<Pokemon>();

                        foreach (Box box in boxes)
                        {
                            if (!box.Enabled)
                            {
                                disabledBoxes.Add(box);
                            }
                            else
                            {
                                foreach (Pokemon pokemon in box.Pokemons)
                                {
                                    if (!pokemon.Enabled)
                                    {
                                        disabledPokemon.Add(pokemon);
                                    }
                                }
                            }
                        }

                        if (modeComboBox.SelectedIndex == 0)
                        {
                            switch (doubleCheckBox.Checked)
                            {
                                case true:
                                    sw.WriteLine($"Double Team Addtions: Before Level {doubleLevelNumericUpDown.Value}");
                                    break;
                                case false:
                                    sw.WriteLine("Double Team Addtitions: Disabled");
                                    break;
                            }
                        }
                        sw.WriteLine($"DLC Bosses: {dlcCheckBox.Checked}");
                        sw.WriteLine();

                        sw.Write($"Disabled Boxes: ");

                        if (!disabledBoxes.Any())
                        {
                            sw.WriteLine("None");
                        }
                        else
                        {
                            for (int i = 0; i < disabledBoxes.Count; i++)
                            {
                                if (i == disabledBoxes.Count - 1)
                                {
                                    sw.WriteLine($"{disabledBoxes[i]}");
                                }
                                else
                                {
                                    sw.Write($"{disabledBoxes[i]}, ");
                                }
                            }
                        }

                        sw.Write($"Disabled Pokémon: ");

                        if (!disabledPokemon.Any())
                        {
                            sw.WriteLine("None");
                        }
                        else
                        {
                            for (int i = 0; i < disabledPokemon.Count; i++)
                            {
                                if (i == disabledPokemon.Count - 1)
                                {
                                    sw.WriteLine($"{disabledPokemon[i]}");
                                }
                                else
                                {
                                    sw.Write($"{disabledPokemon[i]}, ");
                                }
                            }
                        }

                        sw.WriteLine();
                    }

                    void AppendAvailables(int level) // Add all boxes and Pokemon available at a certain level to the avaialable lists
                    {
                        foreach (Box box in boxes)
                        {
                            if (box.Enabled & box.Level == level)
                            {
                                availableBoxes.Add(box);
                            }
                        }

                        foreach (Box box in availableBoxes)
                        {
                            foreach (Pokemon pokemon in box.Pokemons)
                            {
                                if (!pokemon.Enabled)
                                {
                                    box.Pokemons.Remove(pokemon);
                                }
                                else
                                {
                                    availablePokemons.Add(pokemon);
                                }
                            }
                        }
                    }

                    List<Pokemon> GenerateTeam()
                    {
                        List<Pokemon> team = new List<Pokemon>();

                        if (diverseTeams.Checked) // Diverse Teams is ON
                        {
                            List<Box> tempBoxes = new List<Box>(availableBoxes);
                            List<Box> teamBoxes = new List<Box>();

                            for (int i = 0; i < 6; i++)
                            {
                                int j = rand.Next(tempBoxes.Count);
                                teamBoxes.Add(tempBoxes[j]); // Get random box.
                                tempBoxes.Remove(teamBoxes[i]);
                                Pokemon choice = teamBoxes[i].Pokemons[rand.Next(teamBoxes[i].Pokemons.Count)]; // Get random Pokemon in box
                                Pokemon teamMember = new Pokemon(choice.Species, choice.Level, choice.Types); // and make a copy of it.

                                GenerateNatureAndTera(teamMember); // Generate its Nature and Tera Type.

                                team.Add(teamMember); // Add it to the team.

                                if (!allowDuplicates.Checked) // If duplicates are NOT allowed
                                {
                                    RemoveFromAvailable(choice); // remove it from available boxes and Pokémon.
                                }
                            }
                        }

                        else // Diverse Teams is OFF
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                Pokemon choice = availablePokemons[rand.Next(availablePokemons.Count)]; // Get random Pokemon
                                Pokemon teamMember = new Pokemon(choice.Species, choice.Level, choice.Types); // and make a copy of it.

                                GenerateNatureAndTera(teamMember); // Generate its Nature and Tera Type.

                                team.Add(teamMember); // Add it to the team.

                                if (!allowDuplicates.Checked) // If duplicates are NOT allowed
                                {
                                    RemoveFromAvailable(choice); // remove it from available boxes and Pokémon.
                                }
                            }
                        }

                        return team;
                    }

                    void GenerateNatureAndTera(Pokemon pokemon)
                    {
                        pokemon.Nature = natures[rand.Next(natures.Count)]; // Give it a random nature

                        if (stabTeraTypes.Checked)
                        {
                            pokemon.TeraType = types[rand.Next(types.Count)]; // and a random Tera Type
                        }
                        else
                        {
                            List<string> tempTypes = new List<string>(types);
                            foreach (string type in pokemon.Types)
                            {
                                tempTypes.Remove(type);
                            }
                            pokemon.TeraType = tempTypes[rand.Next(tempTypes.Count)]; // or a non-STAB Tera Type.
                        }
                    }

                    void RemoveFromAvailable(Pokemon pokemon)
                    {
                        foreach (Box box in availableBoxes)
                        {
                            if (box.Pokemons.Contains(pokemon))
                            {
                                box.Pokemons.Remove(pokemon); // Remove the Pokemon from avaiable boxes
                            }
                        }

                        availablePokemons.Remove(pokemon); // and from available Pokemon.
                    }

                    string FormatPokemon(Pokemon pokemon)
                    {
                        switch (new List<bool> { generateNatures.Checked, generateTeraTypes.Checked })
                        {
                            case [true, true]:
                                return (
                                 $"{pokemon.Species}{new string(' ', pokemonMaxLength - pokemon.Species.Length + 1)}" +
                                 $"({pokemon.Nature}, Tera {pokemon.TeraType})"
                                 );
                            case [true, false]:
                                return (
                                 $"{pokemon.Species}{new string(' ', pokemonMaxLength - pokemon.Species.Length + 1)}" +
                                 $"({pokemon.Nature})"
                                 );
                            case [false, true]:
                                return (
                                 $"{pokemon.Species}{new string(' ', pokemonMaxLength - pokemon.Species.Length + 1)}" +
                                 $"(Tera {pokemon.TeraType})"
                                 );
                            default:
                                return (pokemon.Species);
                        }
                    }

                    string FormatBoss(Boss boss)
                    {
                        return $"{boss.Name}:{new string(' ', bossMaxLength - boss.Name.Length + 1)}";
                    }

                    void GenerateTeraRerolls()
                    {
                        sw.WriteLine("Tera Type Rerolls");

                        for (int i = 0; i < 50; i++)
                        {
                            string type = types[rand.Next(types.Count)];
                            sw.WriteLine($"   {type}");
                        }
                    }
                }
            }
        }

        private void seedComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            switch (seedComboBox.SelectedIndex)
            {
                case 0: // Custom
                    seedNumericUpDown.Value = 0;
                    seedNumericUpDown.Enabled = true;
                    break;
                case 1: // Daily
                    DateTime dateTime = DateTime.Now;
                    DateTime date = dateTime.Date;
                    seedNumericUpDown.Value = Convert.ToInt32(date.ToString("yyyyMMdd"));
                    seedNumericUpDown.Enabled = false;
                    break;
                case 2: // Random
                    Random seedGenerator = new Random();
                    seedNumericUpDown.Value = seedGenerator.Next();
                    seedNumericUpDown.Enabled = false;
                    break;
            }
        }

        private void doubleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            doubleLevelNumericUpDown.Enabled = doubleCheckBox.Checked;
        }
    }
}
