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
        private Trainer trainer = null;
        private TTTReport report = null;
        private TrainerSettings trainerSettings = new TrainerSettings();
        private int intMaxSmootherQueueSize = 100;
        private Smoother smoother = null;

        //constants
        private string strBadGames = "Bad";
        private string strWonGames = "Won";
        private string strValidMoves = "Valid";
        private string strInvalidMoves = "Invalid";
        
        /// <summary>
        /// 
        /// </summary>
        public TrainerGUI()
        {
            InitializeComponent();
            smoother = new Smoother(intMaxSmootherQueueSize);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ofdFiles.Title = "Select ML Project - File Selection";
                this.ofdFiles.Filter = "ML Project File (*.MLGame)|*.MLGame|All Files (*.*)|*.*";
                if (this.ofdFiles.ShowDialog() == DialogResult.OK)
                {
                    string strFileName = this.ofdFiles.FileName;
                    string strContents = System.IO.File.ReadAllText(strFileName);
                    this.trainerSettings = SerializerTools.DeserializeItem<TrainerSettings>(strContents);
                    this.PopulateUI();
                }
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
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.sfdFiles.Title = "Select ML Project File - File Selection";
                this.sfdFiles.Filter = "ML Project File (*.MLGame)|*.MLGame|All Files (*.*)|*.*";
                if (this.sfdFiles.ShowDialog() == DialogResult.OK)
                {
                    this.PopulateSettings();
                    string strFileName = this.sfdFiles.FileName;
                    string strContents = SerializerTools.SerializeItem<TrainerSettings>(this.trainerSettings);
                    System.IO.File.WriteAllText(strFileName, strContents);
                }
            }
            catch (Exception ex)
            {
                this.DisplayMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateUI()
        {
            this.txtActivations.Text = this.trainerSettings.Activations;
            this.txtLayers.Text = this.trainerSettings.Layers;
            this.txtMutationChance.Text = this.trainerSettings.MutationChance.ToString();
            this.txtMutationStrength.Text = this.trainerSettings.MutionStrength.ToString();
            this.txtPopulationSize.Text = this.trainerSettings.PopulationSize.ToString();
            this.txtReportFolder.Text = this.trainerSettings.ReportFolder;
            this.txtSaveFrequency.Text = this.trainerSettings.SaveWeightFileFrequency.ToString();
            this.txtTrainingReportFrequency.Text = this.trainerSettings.WriteToReportFrequency.ToString();
            this.txtWeightFileIn.Text = this.trainerSettings.WeightFileIn;
            this.txtWeightFileOut.Text = this.trainerSettings.WeightFileOutBase;
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateSettings()
        {
            this.trainerSettings.Layers = this.txtLayers.Text;
            this.trainerSettings.Activations = this.txtActivations.Text;
            this.trainerSettings.PopulationSize = int.Parse(this.txtPopulationSize.Text);
            this.trainerSettings.MutationChance = float.Parse(this.txtMutationChance.Text);
            this.trainerSettings.MutionStrength = float.Parse(this.txtMutationStrength.Text);
            this.trainerSettings.WeightFileIn = this.txtWeightFileIn.Text;
            this.trainerSettings.WeightFileOutBase = this.txtWeightFileOut.Text;
            this.trainerSettings.SaveWeightFileFrequency = int.Parse(this.txtSaveFrequency.Text);
            this.trainerSettings.ReportFolder = this.txtReportFolder.Text;
            this.trainerSettings.WriteToReportFrequency = int.Parse(this.txtTrainingReportFrequency.Text);
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
        private void btnBrowseTrainingReportFolder_Click(object sender, EventArgs e)
        {
            this.fbdFolders.Description = "Select Training Report Folder";
            if (this.fbdFolders.ShowDialog() == DialogResult.OK)
            {
                this.txtReportFolder.Text = this.fbdFolders.SelectedPath;
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
            networkDesigner.SetNetworkStructure(this.txtLayers.Text, this.txtActivations.Text);
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
                if (this.tmrPace.Enabled == false)
                {
                    this.ClearDisplay();
                    this.DisplayMsg("Started Training");
                    //start the run
                    this.StartTraining();
                }
                else
                {
                    //stop the run
                    this.StopTraining();
                }
      
            }
            catch (Exception ex)
            {
                this.DisplayMsg(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartTraining()
        {

            //get input from the ui
            this.PopulateSettings();

            //create the trainer
            this.trainer = new Trainer(this.trainerSettings);

            string strReportFile = string.Format("{0}/ReportFile{1:yyyyMMddhhmmss}.csv", this.trainerSettings.ReportFolder, DateTime.Now);
            this.report = new TTTReport(this.trainer, strReportFile);

            this.report.StartReport();
            //start the timer
            this.tmrPace.Enabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        private void StopTraining()
        {
            this.tmrPace.Enabled = false;
            this.DisplayMsg("Stopped");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrPace_Tick(object sender, EventArgs e)
        {
            this.tmrPace.Enabled = false;

            try
            {
                //play all turns of the generation
                while (this.trainer.PlayTurn())
                {
                    //generation being processed
                }
                if (this.trainer.GamesRunning == false)
                {
                    this.lblGeneration.Text = this.trainer.Generation.ToString();
                    this.lblInvalidMoves.Text = this.smoother.GetValue(this.strInvalidMoves, this.trainer.InvalidMoves).ToString();
                    this.lblValidMoves.Text = this.smoother.GetValue(this.strValidMoves, this.trainer.ValidMoves).ToString();
                    this.lblWins.Text = this.smoother.GetValue(this.strWonGames, this.trainer.Wins).ToString();
                    this.lblDraws.Text = this.trainer.Draws.ToString();
                    this.lblBadGames.Text = this.smoother.GetValue(this.strBadGames, this.trainer.BadGames).ToString();
                    //update the report
                    this.report.Update();
                    //set the reward
                    this.trainer.Reward();
                }

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
        private void btnTestHumanGame_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                this.DisplayMsg(ex.ToString());
            }
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
        private void ClearDisplay()
        {
            this.txtStatus.Text = string.Empty;
        }
    }
}
