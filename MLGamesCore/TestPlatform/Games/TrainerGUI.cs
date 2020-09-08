﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestPlatform.Games
{
    public partial class TrainerGUI : Form
    {
        Trainer trainer = null;
        public TrainerGUI()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.trainer = new Trainer();
            this.tmrPace.Enabled = true;
        }

        private void tmrPace_Tick(object sender, EventArgs e)
        {
            this.tmrPace.Enabled = false;
            //play all turns of the generation
            while (this.trainer.PlayTurn())
            {
                //generation being processed
            }
            if (this.trainer.GamesRunning == false)
            {
                this.lblGeneration.Text = this.trainer.Generation.ToString();
                this.lblInvalidMoves.Text = this.trainer.InvalidMoves.ToString();
                this.lblValidMoves.Text = this.trainer.ValidMoves.ToString();
                this.lblWins.Text = this.trainer.Wins.ToString();
                this.lblDraws.Text = this.trainer.Draws.ToString();
                this.lblBadGames.Text = this.trainer.BadGames.ToString();
                this.trainer.Reward();
            }

            this.tmrPace.Enabled = true;
        }
    }
}
