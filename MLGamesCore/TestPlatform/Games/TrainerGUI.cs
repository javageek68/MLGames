using MLGames;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestPlatform.Common;
using static MLGames.NNSettings;

namespace TestPlatform.Games
{
    public partial class TrainerGUI : Form
    {
        /// <summary>
        /// 
        /// </summary>
        Trainer trainer = null;
        TTTReport report = null;
        
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                //get input from the ui
                int[] layers = StringUtils.ParseIntArray(this.txtLayers.Text);
                ActivationFunctions[] activation = (ActivationFunctions[])(object)StringUtils.ParseIntArray(this.txtActivations.Text);
                int populationSize = int.Parse(this.txtPopulationSize.Text);
                float mutationChance = float.Parse(this.txtMutationChance.Text);
                float mutationStrength = float.Parse(this.txtMutationStrength.Text);
                string strWeightFileIn = this.txtWeightFileIn.Text;
                string strBaseWeightFileOut = this.txtWeightFileOut.Text;
                int intSaveWeightsFrequency = int.Parse(this.txtSaveFrequency.Text);
                string strReportFolder = this.txtReportFolder.Text;

                //create the trainer
                this.trainer = new Trainer(layers, activation, populationSize, mutationChance, mutationStrength, strWeightFileIn, strBaseWeightFileOut, intSaveWeightsFrequency);

                string strReportFile = string.Format("{0}/ReportFile{1:yyyyMMddhhmmss}.csv", strReportFolder, DateTime.Now);
                this.report = new TTTReport(this.trainer, strReportFile);

                this.report.StartReport();
                //start the timer
                this.tmrPace.Enabled = true;
            }
            catch (Exception ex)
            {
                this.DisplayMsg(ex.ToString());
            }

 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                //update the report
                this.report.Update();
                //set the reward
                this.trainer.Reward();
            }

            this.tmrPace.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strMsg"></param>
        private void DisplayMsg(string strMsg)
        {
            this.txtStatus.AppendText(strMsg + "\r\n");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestHumanGame_Click(object sender, EventArgs e)
        {
            string strWeightFileIn = this.txtWeightFileIn.Text;
            if (strWeightFileIn.Trim().Length == 0)
            {
                this.DisplayMsg("Select a weight file first");
                return;
            }
            NeuralNetwork nn = new NeuralNetwork(strWeightFileIn);
            TTTHumanVsAI humanGame = new TTTHumanVsAI();
            humanGame.computerPlayer = nn;
            humanGame.Show();
        }
    }
}
