namespace TestPlatform.Games
{
    partial class TrainerGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.tmrPace = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblInvalidMoves = new System.Windows.Forms.Label();
            this.lblValidMoves = new System.Windows.Forms.Label();
            this.lblGeneration = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDraws = new System.Windows.Forms.Label();
            this.lblWins = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblBadGames = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.gbStats = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.gbGeneticParms = new System.Windows.Forms.GroupBox();
            this.txtTrainingReportFrequency = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnBrowseTrainingReportFolder = new System.Windows.Forms.Button();
            this.txtReportFolder = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSaveFrequency = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.btnWeightFileOut = new System.Windows.Forms.Button();
            this.btnBrowseWeightFileIn = new System.Windows.Forms.Button();
            this.txtWeightFileOut = new System.Windows.Forms.TextBox();
            this.txtWeightFileIn = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtPopulationSize = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMutationStrength = new System.Windows.Forms.TextBox();
            this.txtMutationChance = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtActivations = new System.Windows.Forms.TextBox();
            this.txtLayers = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.ofdFiles = new System.Windows.Forms.OpenFileDialog();
            this.sfdFiles = new System.Windows.Forms.SaveFileDialog();
            this.btnTestHumanGame = new System.Windows.Forms.Button();
            this.fbdFolders = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbStats.SuspendLayout();
            this.gbGeneticParms.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.Color.Black;
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(20, 317);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(763, 115);
            this.txtStatus.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.Location = new System.Drawing.Point(18, 39);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 28);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start Training";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // tmrPace
            // 
            this.tmrPace.Interval = 10;
            this.tmrPace.Tick += new System.EventHandler(this.tmrPace_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Valid Moves";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Invalid Moves";
            // 
            // lblInvalidMoves
            // 
            this.lblInvalidMoves.AutoSize = true;
            this.lblInvalidMoves.Location = new System.Drawing.Point(104, 35);
            this.lblInvalidMoves.Name = "lblInvalidMoves";
            this.lblInvalidMoves.Size = new System.Drawing.Size(13, 13);
            this.lblInvalidMoves.TabIndex = 7;
            this.lblInvalidMoves.Text = "0";
            // 
            // lblValidMoves
            // 
            this.lblValidMoves.AutoSize = true;
            this.lblValidMoves.Location = new System.Drawing.Point(104, 18);
            this.lblValidMoves.Name = "lblValidMoves";
            this.lblValidMoves.Size = new System.Drawing.Size(13, 13);
            this.lblValidMoves.TabIndex = 6;
            this.lblValidMoves.Text = "0";
            // 
            // lblGeneration
            // 
            this.lblGeneration.AutoSize = true;
            this.lblGeneration.Location = new System.Drawing.Point(253, 54);
            this.lblGeneration.Name = "lblGeneration";
            this.lblGeneration.Size = new System.Drawing.Size(13, 13);
            this.lblGeneration.TabIndex = 9;
            this.lblGeneration.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(155, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Generation";
            // 
            // lblDraws
            // 
            this.lblDraws.AutoSize = true;
            this.lblDraws.Location = new System.Drawing.Point(253, 18);
            this.lblDraws.Name = "lblDraws";
            this.lblDraws.Size = new System.Drawing.Size(13, 13);
            this.lblDraws.TabIndex = 13;
            this.lblDraws.Text = "0";
            // 
            // lblWins
            // 
            this.lblWins.AutoSize = true;
            this.lblWins.Location = new System.Drawing.Point(104, 54);
            this.lblWins.Name = "lblWins";
            this.lblWins.Size = new System.Drawing.Size(13, 13);
            this.lblWins.TabIndex = 12;
            this.lblWins.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(155, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Draws";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 54);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Wins";
            // 
            // lblBadGames
            // 
            this.lblBadGames.AutoSize = true;
            this.lblBadGames.Location = new System.Drawing.Point(253, 33);
            this.lblBadGames.Name = "lblBadGames";
            this.lblBadGames.Size = new System.Drawing.Size(13, 13);
            this.lblBadGames.TabIndex = 15;
            this.lblBadGames.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(155, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Bad Games";
            // 
            // gbStats
            // 
            this.gbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStats.Controls.Add(this.label2);
            this.gbStats.Controls.Add(this.lblGeneration);
            this.gbStats.Controls.Add(this.lblBadGames);
            this.gbStats.Controls.Add(this.label4);
            this.gbStats.Controls.Add(this.label1);
            this.gbStats.Controls.Add(this.label5);
            this.gbStats.Controls.Add(this.lblValidMoves);
            this.gbStats.Controls.Add(this.lblDraws);
            this.gbStats.Controls.Add(this.lblInvalidMoves);
            this.gbStats.Controls.Add(this.label6);
            this.gbStats.Controls.Add(this.lblWins);
            this.gbStats.Controls.Add(this.label7);
            this.gbStats.ForeColor = System.Drawing.Color.White;
            this.gbStats.Location = new System.Drawing.Point(488, 39);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(295, 83);
            this.gbStats.TabIndex = 16;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "Stats";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mutation Chance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Mutation Strength";
            // 
            // gbGeneticParms
            // 
            this.gbGeneticParms.Controls.Add(this.txtTrainingReportFrequency);
            this.gbGeneticParms.Controls.Add(this.label16);
            this.gbGeneticParms.Controls.Add(this.btnBrowseTrainingReportFolder);
            this.gbGeneticParms.Controls.Add(this.txtReportFolder);
            this.gbGeneticParms.Controls.Add(this.label15);
            this.gbGeneticParms.Controls.Add(this.txtSaveFrequency);
            this.gbGeneticParms.Controls.Add(this.label14);
            this.gbGeneticParms.Controls.Add(this.btnWeightFileOut);
            this.gbGeneticParms.Controls.Add(this.btnBrowseWeightFileIn);
            this.gbGeneticParms.Controls.Add(this.txtWeightFileOut);
            this.gbGeneticParms.Controls.Add(this.txtWeightFileIn);
            this.gbGeneticParms.Controls.Add(this.label13);
            this.gbGeneticParms.Controls.Add(this.label12);
            this.gbGeneticParms.Controls.Add(this.txtPopulationSize);
            this.gbGeneticParms.Controls.Add(this.label11);
            this.gbGeneticParms.Controls.Add(this.txtMutationStrength);
            this.gbGeneticParms.Controls.Add(this.txtMutationChance);
            this.gbGeneticParms.Controls.Add(this.label8);
            this.gbGeneticParms.Controls.Add(this.label3);
            this.gbGeneticParms.ForeColor = System.Drawing.Color.White;
            this.gbGeneticParms.Location = new System.Drawing.Point(176, 39);
            this.gbGeneticParms.Name = "gbGeneticParms";
            this.gbGeneticParms.Size = new System.Drawing.Size(271, 227);
            this.gbGeneticParms.TabIndex = 19;
            this.gbGeneticParms.TabStop = false;
            this.gbGeneticParms.Text = "Genetic Hyperparameters";
            // 
            // txtTrainingReportFrequency
            // 
            this.txtTrainingReportFrequency.Location = new System.Drawing.Point(142, 181);
            this.txtTrainingReportFrequency.Name = "txtTrainingReportFrequency";
            this.txtTrainingReportFrequency.Size = new System.Drawing.Size(86, 20);
            this.txtTrainingReportFrequency.TabIndex = 34;
            this.txtTrainingReportFrequency.Text = "100";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 185);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(133, 13);
            this.label16.TabIndex = 33;
            this.label16.Text = "Training Report Frequency";
            // 
            // btnBrowseTrainingReportFolder
            // 
            this.btnBrowseTrainingReportFolder.BackColor = System.Drawing.Color.Black;
            this.btnBrowseTrainingReportFolder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBrowseTrainingReportFolder.Location = new System.Drawing.Point(234, 159);
            this.btnBrowseTrainingReportFolder.Name = "btnBrowseTrainingReportFolder";
            this.btnBrowseTrainingReportFolder.Size = new System.Drawing.Size(31, 21);
            this.btnBrowseTrainingReportFolder.TabIndex = 32;
            this.btnBrowseTrainingReportFolder.Text = "...";
            this.btnBrowseTrainingReportFolder.UseVisualStyleBackColor = false;
            this.btnBrowseTrainingReportFolder.Click += new System.EventHandler(this.btnBrowseTrainingReportFolder_Click);
            // 
            // txtReportFolder
            // 
            this.txtReportFolder.Location = new System.Drawing.Point(142, 159);
            this.txtReportFolder.Name = "txtReportFolder";
            this.txtReportFolder.Size = new System.Drawing.Size(86, 20);
            this.txtReportFolder.TabIndex = 31;
            this.txtReportFolder.Text = "D:\\Code\\UnityProjects\\MLGames\\Reports\\TicTacToe";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 163);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 13);
            this.label15.TabIndex = 30;
            this.label15.Text = "Report Folder";
            // 
            // txtSaveFrequency
            // 
            this.txtSaveFrequency.Location = new System.Drawing.Point(142, 137);
            this.txtSaveFrequency.Name = "txtSaveFrequency";
            this.txtSaveFrequency.Size = new System.Drawing.Size(86, 20);
            this.txtSaveFrequency.TabIndex = 29;
            this.txtSaveFrequency.Text = "1000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 141);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(127, 13);
            this.label14.TabIndex = 28;
            this.label14.Text = "Save Weights Frequency";
            // 
            // btnWeightFileOut
            // 
            this.btnWeightFileOut.BackColor = System.Drawing.Color.Black;
            this.btnWeightFileOut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnWeightFileOut.Location = new System.Drawing.Point(234, 113);
            this.btnWeightFileOut.Name = "btnWeightFileOut";
            this.btnWeightFileOut.Size = new System.Drawing.Size(31, 21);
            this.btnWeightFileOut.TabIndex = 27;
            this.btnWeightFileOut.Text = "...";
            this.btnWeightFileOut.UseVisualStyleBackColor = false;
            this.btnWeightFileOut.Click += new System.EventHandler(this.btnWeightFileOut_Click);
            // 
            // btnBrowseWeightFileIn
            // 
            this.btnBrowseWeightFileIn.BackColor = System.Drawing.Color.Black;
            this.btnBrowseWeightFileIn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnBrowseWeightFileIn.Location = new System.Drawing.Point(234, 88);
            this.btnBrowseWeightFileIn.Name = "btnBrowseWeightFileIn";
            this.btnBrowseWeightFileIn.Size = new System.Drawing.Size(31, 21);
            this.btnBrowseWeightFileIn.TabIndex = 24;
            this.btnBrowseWeightFileIn.Text = "...";
            this.btnBrowseWeightFileIn.UseVisualStyleBackColor = false;
            this.btnBrowseWeightFileIn.Click += new System.EventHandler(this.btnBrowseWeightFileIn_Click);
            // 
            // txtWeightFileOut
            // 
            this.txtWeightFileOut.Location = new System.Drawing.Point(142, 113);
            this.txtWeightFileOut.Name = "txtWeightFileOut";
            this.txtWeightFileOut.Size = new System.Drawing.Size(86, 20);
            this.txtWeightFileOut.TabIndex = 26;
            this.txtWeightFileOut.Text = "D:\\Code\\UnityProjects\\MLGames\\WeightFiles\\TicTacToeFiles\\WeightFile";
            // 
            // txtWeightFileIn
            // 
            this.txtWeightFileIn.Location = new System.Drawing.Point(142, 88);
            this.txtWeightFileIn.Name = "txtWeightFileIn";
            this.txtWeightFileIn.Size = new System.Drawing.Size(86, 20);
            this.txtWeightFileIn.TabIndex = 25;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 117);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Weight File Out Base";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 92);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Weight File In";
            // 
            // txtPopulationSize
            // 
            this.txtPopulationSize.Location = new System.Drawing.Point(142, 64);
            this.txtPopulationSize.Name = "txtPopulationSize";
            this.txtPopulationSize.Size = new System.Drawing.Size(86, 20);
            this.txtPopulationSize.TabIndex = 22;
            this.txtPopulationSize.Text = "100";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 69);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(80, 13);
            this.label11.TabIndex = 21;
            this.label11.Text = "Population Size";
            // 
            // txtMutationStrength
            // 
            this.txtMutationStrength.Location = new System.Drawing.Point(142, 40);
            this.txtMutationStrength.Name = "txtMutationStrength";
            this.txtMutationStrength.Size = new System.Drawing.Size(86, 20);
            this.txtMutationStrength.TabIndex = 20;
            this.txtMutationStrength.Text = "0.5";
            // 
            // txtMutationChance
            // 
            this.txtMutationChance.Location = new System.Drawing.Point(142, 16);
            this.txtMutationChance.Name = "txtMutationChance";
            this.txtMutationChance.Size = new System.Drawing.Size(86, 20);
            this.txtMutationChance.TabIndex = 19;
            this.txtMutationChance.Text = "0.01";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtActivations);
            this.groupBox1.Controls.Add(this.txtLayers);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(488, 134);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(276, 66);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Network Structure";
            // 
            // txtActivations
            // 
            this.txtActivations.Location = new System.Drawing.Point(158, 34);
            this.txtActivations.Name = "txtActivations";
            this.txtActivations.Size = new System.Drawing.Size(86, 20);
            this.txtActivations.TabIndex = 22;
            this.txtActivations.Text = "1,1";
            // 
            // txtLayers
            // 
            this.txtLayers.Location = new System.Drawing.Point(158, 12);
            this.txtLayers.Name = "txtLayers";
            this.txtLayers.Size = new System.Drawing.Size(86, 20);
            this.txtLayers.TabIndex = 21;
            this.txtLayers.Text = "9, 18, 9";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(81, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "Activations";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(81, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "Layers";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Black;
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnEdit.Location = new System.Drawing.Point(9, 19);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(61, 28);
            this.btnEdit.TabIndex = 21;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnTestHumanGame
            // 
            this.btnTestHumanGame.BackColor = System.Drawing.Color.Black;
            this.btnTestHumanGame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnTestHumanGame.Location = new System.Drawing.Point(20, 265);
            this.btnTestHumanGame.Name = "btnTestHumanGame";
            this.btnTestHumanGame.Size = new System.Drawing.Size(120, 28);
            this.btnTestHumanGame.TabIndex = 21;
            this.btnTestHumanGame.Text = "Test Human Game";
            this.btnTestHumanGame.UseVisualStyleBackColor = false;
            this.btnTestHumanGame.Click += new System.EventHandler(this.btnTestHumanGame_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 22;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // TrainerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTestHumanGame);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbGeneticParms);
            this.Controls.Add(this.gbStats);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.menuStrip1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TrainerGUI";
            this.Text = "TrainerGUI";
            this.gbStats.ResumeLayout(false);
            this.gbStats.PerformLayout();
            this.gbGeneticParms.ResumeLayout(false);
            this.gbGeneticParms.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer tmrPace;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblInvalidMoves;
        private System.Windows.Forms.Label lblValidMoves;
        private System.Windows.Forms.Label lblGeneration;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDraws;
        private System.Windows.Forms.Label lblWins;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblBadGames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox gbStats;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbGeneticParms;
        private System.Windows.Forms.TextBox txtMutationStrength;
        private System.Windows.Forms.TextBox txtMutationChance;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtActivations;
        private System.Windows.Forms.TextBox txtLayers;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.TextBox txtPopulationSize;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnWeightFileOut;
        private System.Windows.Forms.Button btnBrowseWeightFileIn;
        private System.Windows.Forms.TextBox txtWeightFileOut;
        private System.Windows.Forms.TextBox txtWeightFileIn;
        private System.Windows.Forms.OpenFileDialog ofdFiles;
        private System.Windows.Forms.SaveFileDialog sfdFiles;
        private System.Windows.Forms.TextBox txtSaveFrequency;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnTestHumanGame;
        private System.Windows.Forms.Button btnBrowseTrainingReportFolder;
        private System.Windows.Forms.TextBox txtReportFolder;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTrainingReportFrequency;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.FolderBrowserDialog fbdFolders;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
    }
}