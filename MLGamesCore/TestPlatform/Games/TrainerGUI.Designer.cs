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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.gbStats.SuspendLayout();
            this.gbGeneticParms.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.BackColor = System.Drawing.Color.Black;
            this.txtStatus.ForeColor = System.Drawing.Color.White;
            this.txtStatus.Location = new System.Drawing.Point(20, 219);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(763, 213);
            this.txtStatus.TabIndex = 3;
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.Black;
            this.btnStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnStart.Location = new System.Drawing.Point(18, 18);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(102, 28);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
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
            this.gbStats.Location = new System.Drawing.Point(488, 18);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(295, 83);
            this.gbStats.TabIndex = 16;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "Stats";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Mutation Chance";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Mutation Rate";
            // 
            // gbGeneticParms
            // 
            this.gbGeneticParms.Controls.Add(this.textBox2);
            this.gbGeneticParms.Controls.Add(this.textBox1);
            this.gbGeneticParms.Controls.Add(this.label8);
            this.gbGeneticParms.Controls.Add(this.label3);
            this.gbGeneticParms.ForeColor = System.Drawing.Color.White;
            this.gbGeneticParms.Location = new System.Drawing.Point(138, 18);
            this.gbGeneticParms.Name = "gbGeneticParms";
            this.gbGeneticParms.Size = new System.Drawing.Size(271, 70);
            this.gbGeneticParms.TabIndex = 19;
            this.gbGeneticParms.TabStop = false;
            this.gbGeneticParms.Text = "Gen Parms";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(142, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(86, 20);
            this.textBox1.TabIndex = 19;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(142, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(86, 20);
            this.textBox2.TabIndex = 20;
            // 
            // TrainerGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gbGeneticParms);
            this.Controls.Add(this.gbStats);
            this.Controls.Add(this.txtStatus);
            this.Controls.Add(this.btnStart);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "TrainerGUI";
            this.Text = "TrainerGUI";
            this.gbStats.ResumeLayout(false);
            this.gbStats.PerformLayout();
            this.gbGeneticParms.ResumeLayout(false);
            this.gbGeneticParms.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
    }
}