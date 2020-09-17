using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestPlatform.Common;

namespace TestPlatform.Games
{
    /// <summary>
    /// 
    /// </summary>
    public class TTTReport
    {
        Trainer trainer = null;
        string strFileName = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainer"></param>
        /// <param name="strFileName"></param>
        public TTTReport(Trainer trainer, string strFileName)
        {
            this.trainer = trainer;
            this.strFileName = strFileName;
        }

        /// <summary>
        /// 
        /// </summary>
        public void StartReport()
        {
            string strHeaders = "Generation, valid, invalid, wins, draws, badGames,,Activations, Layers, MutationChance, MutationStrength, PopulationSize\r\n";
            TrainerSettings settings = this.trainer.settings;
            string strFirstLine = string.Format("0,0,0,0,0,0,,\"{0}\",\"{1}\",{2},{3},{4}\r\n", settings.GetActivationNameList(), settings.Layers, settings.MutationChance, settings.MutionStrength, settings.PopulationSize);
            System.IO.File.WriteAllText(strFileName, strHeaders);
            System.IO.File.AppendAllText(strFileName, strFirstLine);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Update()
        {
            //only write to the report once every WriteToReportFrequency generations
            if (this.trainer.Generation % this.trainer.settings.WriteToReportFrequency == 0)
            {
                //write to report file
                string strLine = string.Format("{0},{1},{2},{3},{4},{5}\r\n", trainer.Generation, trainer.ValidMoves, trainer.InvalidMoves, trainer.Wins, trainer.Draws, trainer.BadGames);
                System.IO.File.AppendAllText(strFileName, strLine);
            }
  
        }

    }
}
