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
            this.trainer.Play();
            this.lblGeneration.Text = this.trainer.Generation.ToString();
            this.lblInvalidMoves.Text = this.trainer.InvalidMoves.ToString();
            this.lblValidMoves.Text = this.trainer.ValidMoves.ToString();
            this.tmrPace.Enabled = true;
        }
    }
}
