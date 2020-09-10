using MLGames;
using System;
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
        /// <summary>
        /// 
        /// </summary>
        Trainer trainer = null;
        
        /// <summary>
        /// 
        /// </summary>
        public TrainerGUI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWeightFileOut_Click(object sender, EventArgs e)
        {
            this.sfdFiles.Title = "Select Weight File - File Selection";
            this.sfdFiles.Filter = "Weight File (*.wght)|*.wght|All Files (*.*)|*.*";
            if (this.sfdFiles.ShowDialog()== DialogResult.OK)
            {
                this.txtWeightFileOut.Text = this.sfdFiles.FileName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowseWeightFileIn_Click(object sender, EventArgs e)
        {
            this.ofdFiles.Title = "Select Weight File - File Selection";
            this.ofdFiles.Filter = "Weight File (*.wght)|*.wght|All Files (*.*)|*.*";
            if (this.ofdFiles.ShowDialog()== DialogResult.OK )
            {
                this.txtWeightFileIn.Text = this.ofdFiles.FileName;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int[] layers = new int[0];
            int[] activationFunctions = new int[0];

            //show the network designer
            NetworkDesigner networkDesigner = new NetworkDesigner();
            if (networkDesigner.ShowDialog() == DialogResult.OK)
            {
                //get the structure
                networkDesigner.GetNetworkStructure(ref layers, ref activationFunctions);

                //display the structure in the gui
                this.txtLayers.Text = NNTools.ComposeLayerList(layers);
                this.txtActivations.Text = NNTools.ComposeActivationList(activationFunctions);
            }
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

   

        private void txtWeightFileOut_TextChanged(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }


    }
}
