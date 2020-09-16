using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatform.Games
{
    public class TTTReport
    {
        Trainer trainer = null;
        string strFileName = string.Empty;

        public TTTReport(Trainer trainer, string strFileName)
        {
            this.trainer = trainer;
            this.strFileName = strFileName;
        }

        public void StartReport()
        {
            string strHeaders = "Generation, valid, invalid, wins, draws, badGames\r\n";
            System.IO.File.WriteAllText(strFileName, strHeaders);
        }

        public void Update()
        {
            //write to report file
            string strLine = string.Format("{0},{1},{2},{3},{4},{5}\r\n", trainer.Generation, trainer.ValidMoves, trainer.InvalidMoves, trainer.Wins, trainer.Draws, trainer.BadGames);
            System.IO.File.AppendAllText(strFileName, strLine);
        }

    }
}
