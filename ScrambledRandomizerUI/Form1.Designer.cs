namespace ScrambledRandomizer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            seedLabel = new Label();
            allowDuplicates = new CheckBox();
            diverseTeams = new CheckBox();
            modeLabel = new Label();
            toolTip = new ToolTip(components);
            modeComboBox = new ComboBox();
            seedComboBox = new ComboBox();
            stabTeraTypes = new CheckBox();
            dlcCheckBox = new CheckBox();
            doubleCheckBox = new CheckBox();
            generateNatures = new CheckBox();
            generateTeraTypes = new CheckBox();
            boxesCheckedList = new CheckedListBox();
            pokemonCheckedList = new CheckedListBox();
            generateSeed = new Button();
            seedNumericUpDown = new NumericUpDown();
            pathLabel = new Label();
            pathComboBox = new ComboBox();
            doubleLevelNumericUpDown = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)seedNumericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)doubleLevelNumericUpDown).BeginInit();
            SuspendLayout();
            // 
            // seedLabel
            // 
            seedLabel.AutoSize = true;
            seedLabel.Location = new Point(12, 15);
            seedLabel.Name = "seedLabel";
            seedLabel.Size = new Size(32, 15);
            seedLabel.TabIndex = 0;
            seedLabel.Text = "Seed";
            // 
            // allowDuplicates
            // 
            allowDuplicates.AutoSize = true;
            allowDuplicates.Location = new Point(12, 99);
            allowDuplicates.Name = "allowDuplicates";
            allowDuplicates.Size = new Size(114, 19);
            allowDuplicates.TabIndex = 2;
            allowDuplicates.Text = "Allow Duplicates";
            toolTip.SetToolTip(allowDuplicates, "Allow the same Pokémon to appear multiple times in the seed.\r\nIf this is disabled, the seed may not generate if there are not\r\nenough enabled Pokémon to fill all slots.");
            allowDuplicates.UseVisualStyleBackColor = true;
            // 
            // diverseTeams
            // 
            diverseTeams.AutoSize = true;
            diverseTeams.Checked = true;
            diverseTeams.CheckState = CheckState.Checked;
            diverseTeams.Location = new Point(12, 124);
            diverseTeams.Name = "diverseTeams";
            diverseTeams.Size = new Size(101, 19);
            diverseTeams.TabIndex = 3;
            diverseTeams.Text = "Diverse Teams";
            toolTip.SetToolTip(diverseTeams, "Ensure each starter is selected from a different box.\r\nAlso applies to each random team in Unique Teams mode.");
            diverseTeams.UseVisualStyleBackColor = true;
            // 
            // modeLabel
            // 
            modeLabel.AutoSize = true;
            modeLabel.Location = new Point(12, 44);
            modeLabel.Name = "modeLabel";
            modeLabel.Size = new Size(38, 15);
            modeLabel.TabIndex = 5;
            modeLabel.Text = "Mode";
            modeLabel.Click += label2_Click;
            // 
            // toolTip
            // 
            toolTip.Popup += toolTipSeed_Popup;
            // 
            // modeComboBox
            // 
            modeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modeComboBox.FormattingEnabled = true;
            modeComboBox.Items.AddRange(new object[] { "Teamlocked", "Unique Teams" });
            modeComboBox.Location = new Point(56, 41);
            modeComboBox.Name = "modeComboBox";
            modeComboBox.Size = new Size(121, 23);
            modeComboBox.TabIndex = 4;
            toolTip.SetToolTip(modeComboBox, "Teamlocked - Fixed team of six Pokémon. Replace team members after they faint.\r\nUnique Teams - For each boss you will be assigned a new team of six Pokémon. Score based on total deaths.");
            modeComboBox.SelectedIndexChanged += modeComboBox_SelectedIndexChanged;
            // 
            // seedComboBox
            // 
            seedComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            seedComboBox.FormattingEnabled = true;
            seedComboBox.Items.AddRange(new object[] { "Custom", "Daily", "Random" });
            seedComboBox.Location = new Point(56, 12);
            seedComboBox.Name = "seedComboBox";
            seedComboBox.Size = new Size(121, 23);
            seedComboBox.TabIndex = 16;
            toolTip.SetToolTip(seedComboBox, "Custom - Input any number as a seed.\r\nDaily - Generate a seed based on today's date.\r\nRandom - Generate a completely random seed.");
            seedComboBox.SelectedIndexChanged += seedComboBox_SelectedIndexChanged_1;
            // 
            // stabTeraTypes
            // 
            stabTeraTypes.AutoSize = true;
            stabTeraTypes.Location = new Point(33, 199);
            stabTeraTypes.Name = "stabTeraTypes";
            stabTeraTypes.Size = new Size(144, 19);
            stabTeraTypes.TabIndex = 9;
            stabTeraTypes.Text = "Allow STAB Tera Types";
            toolTip.SetToolTip(stabTeraTypes, "Allow Pokémon to have one of their types as a\r\nTera Type.");
            stabTeraTypes.UseVisualStyleBackColor = true;
            // 
            // dlcCheckBox
            // 
            dlcCheckBox.AutoSize = true;
            dlcCheckBox.Checked = true;
            dlcCheckBox.CheckState = CheckState.Checked;
            dlcCheckBox.Location = new Point(183, 124);
            dlcCheckBox.Name = "dlcCheckBox";
            dlcCheckBox.Size = new Size(86, 19);
            dlcCheckBox.TabIndex = 19;
            dlcCheckBox.Text = "DLC Battles";
            toolTip.SetToolTip(dlcCheckBox, "Enables bosses from the Teal Mask DLC.\r\nDisable this for a shorter playthrough, or if you\r\ndon't own the DLC.");
            dlcCheckBox.UseVisualStyleBackColor = true;
            // 
            // doubleCheckBox
            // 
            doubleCheckBox.AutoSize = true;
            doubleCheckBox.Checked = true;
            doubleCheckBox.CheckState = CheckState.Checked;
            doubleCheckBox.Location = new Point(183, 149);
            doubleCheckBox.Name = "doubleCheckBox";
            doubleCheckBox.Size = new Size(116, 34);
            doubleCheckBox.TabIndex = 20;
            doubleCheckBox.Text = "Double additions\r\nbefore level:\r\n";
            toolTip.SetToolTip(doubleCheckBox, "If enabled, you will get two Pokémon after bosses\r\nbefore the chosen level instead of one.\r\n(Default: 30)");
            doubleCheckBox.UseVisualStyleBackColor = true;
            doubleCheckBox.CheckedChanged += doubleCheckBox_CheckedChanged;
            // 
            // generateNatures
            // 
            generateNatures.AutoSize = true;
            generateNatures.Checked = true;
            generateNatures.CheckState = CheckState.Checked;
            generateNatures.Location = new Point(12, 149);
            generateNatures.Name = "generateNatures";
            generateNatures.Size = new Size(117, 19);
            generateNatures.TabIndex = 7;
            generateNatures.Text = "Generate Natures";
            generateNatures.UseVisualStyleBackColor = true;
            generateNatures.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // generateTeraTypes
            // 
            generateTeraTypes.AutoSize = true;
            generateTeraTypes.Checked = true;
            generateTeraTypes.CheckState = CheckState.Checked;
            generateTeraTypes.Location = new Point(12, 174);
            generateTeraTypes.Name = "generateTeraTypes";
            generateTeraTypes.Size = new Size(131, 19);
            generateTeraTypes.TabIndex = 8;
            generateTeraTypes.Text = "Generate Tera Types";
            generateTeraTypes.UseVisualStyleBackColor = true;
            generateTeraTypes.CheckedChanged += generateTeraTypes_CheckedChanged;
            // 
            // boxesCheckedList
            // 
            boxesCheckedList.FormattingEnabled = true;
            boxesCheckedList.Location = new Point(12, 229);
            boxesCheckedList.Name = "boxesCheckedList";
            boxesCheckedList.Size = new Size(131, 274);
            boxesCheckedList.TabIndex = 12;
            boxesCheckedList.ItemCheck += boxesCheckedList_ItemCheck;
            boxesCheckedList.SelectedIndexChanged += boxesCheckedList_SelectedIndexChanged;
            // 
            // pokemonCheckedList
            // 
            pokemonCheckedList.Enabled = false;
            pokemonCheckedList.FormattingEnabled = true;
            pokemonCheckedList.Location = new Point(149, 229);
            pokemonCheckedList.Name = "pokemonCheckedList";
            pokemonCheckedList.Size = new Size(165, 274);
            pokemonCheckedList.TabIndex = 13;
            pokemonCheckedList.ItemCheck += pokemonCheckedList_ItemCheck;
            // 
            // generateSeed
            // 
            generateSeed.Location = new Point(12, 509);
            generateSeed.Name = "generateSeed";
            generateSeed.Size = new Size(302, 23);
            generateSeed.TabIndex = 14;
            generateSeed.Text = "Generate Seed";
            generateSeed.UseVisualStyleBackColor = true;
            generateSeed.Click += generateSeed_click;
            // 
            // seedNumericUpDown
            // 
            seedNumericUpDown.Location = new Point(183, 12);
            seedNumericUpDown.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            seedNumericUpDown.Name = "seedNumericUpDown";
            seedNumericUpDown.Size = new Size(90, 23);
            seedNumericUpDown.TabIndex = 15;
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.Location = new Point(12, 73);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(31, 15);
            pathLabel.TabIndex = 17;
            pathLabel.Text = "Path";
            // 
            // pathComboBox
            // 
            pathComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            pathComboBox.FormattingEnabled = true;
            pathComboBox.Items.AddRange(new object[] { "Random" });
            pathComboBox.Location = new Point(56, 70);
            pathComboBox.Name = "pathComboBox";
            pathComboBox.Size = new Size(121, 23);
            pathComboBox.TabIndex = 18;
            // 
            // doubleLevelNumericUpDown
            // 
            doubleLevelNumericUpDown.Location = new Point(264, 189);
            doubleLevelNumericUpDown.Maximum = new decimal(new int[] { 101, 0, 0, 0 });
            doubleLevelNumericUpDown.Name = "doubleLevelNumericUpDown";
            doubleLevelNumericUpDown.Size = new Size(50, 23);
            doubleLevelNumericUpDown.TabIndex = 21;
            doubleLevelNumericUpDown.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(326, 539);
            Controls.Add(doubleLevelNumericUpDown);
            Controls.Add(doubleCheckBox);
            Controls.Add(dlcCheckBox);
            Controls.Add(pathComboBox);
            Controls.Add(pathLabel);
            Controls.Add(seedComboBox);
            Controls.Add(seedNumericUpDown);
            Controls.Add(generateSeed);
            Controls.Add(pokemonCheckedList);
            Controls.Add(boxesCheckedList);
            Controls.Add(stabTeraTypes);
            Controls.Add(generateTeraTypes);
            Controls.Add(generateNatures);
            Controls.Add(modeLabel);
            Controls.Add(modeComboBox);
            Controls.Add(diverseTeams);
            Controls.Add(allowDuplicates);
            Controls.Add(seedLabel);
            Name = "Form1";
            Text = "ScrambledRandomizer";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)seedNumericUpDown).EndInit();
            ((System.ComponentModel.ISupportInitialize)doubleLevelNumericUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label seedLabel;
        private CheckBox allowDuplicates;
        private CheckBox diverseTeams;
        private Label modeLabel;
        private ToolTip toolTip;
        private ComboBox modeComboBox;
        private CheckBox generateNatures;
        private CheckBox generateTeraTypes;
        private CheckBox stabTeraTypes;
        private CheckedListBox boxesCheckedList;
        private CheckedListBox pokemonCheckedList;
        private Button generateSeed;
        private NumericUpDown seedNumericUpDown;
        private ComboBox seedComboBox;
        private Label pathLabel;
        private ComboBox pathComboBox;
        private CheckBox dlcCheckBox;
        private CheckBox doubleCheckBox;
        private NumericUpDown doubleLevelNumericUpDown;
    }
}
