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
    /// <summary>
    /// 
    /// </summary>
    public partial class TTTHumanVsAI : Form
    {
        public NeuralNetwork computerPlayer { get; set; }

        private TicTacToe game = new TicTacToe();
        private List<Button> lstBtnCells = new List<Button>();
        private bool won = false;
        private bool draw = false;
        private float current = 1;
        private float other = 0.5f;

        /// <summary>
        /// 
        /// </summary>
        public TTTHumanVsAI()
        {
            InitializeComponent();
            this.AddBtn(this.btn00);
            this.AddBtn(this.btn10);
            this.AddBtn(this.btn20);


            this.AddBtn(this.btn01);
            this.AddBtn(this.btn11);
            this.AddBtn(this.btn21);

            this.AddBtn(this.btn02);
            this.AddBtn(this.btn12);
            this.AddBtn(this.btn22);

            this.lstBtnCells.Add(this.btn00);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="btn"></param>
        private void AddBtn(Button btn)
        {
            btn.Click += btn_Click;
            this.lstBtnCells.Add(btn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Click(object sender, EventArgs e)
        {
            //get the move from the button tag
            int intMove = int.Parse(((Button)sender).Tag.ToString());
            //check to see if the move is valie
            bool valid = this.game.MakeMove(intMove, ref this.won, ref this.draw);
            if (valid)
            {
                this.lstBtnCells[intMove].Text = "X";
                this.EvalGame();
                if (this.won == false && this.draw == false) this.ComputerMove();
            }
            else
            {
                MessageBox.Show("Player made an invalid move");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ComputerMove()
        {
            //get a copy of the game state
            float[] gameState = new float[this.game.GameState.Length];
            this.game.GameState.CopyTo(gameState, 0);


            //transform the gamestate to the player's perspective
            //in this case, X is other and O is current

            //O is the current player
            NNTools.Transform(ref gameState, this.game.X, this.other);
            NNTools.Transform(ref gameState, this.game.O, this.current);
  
 
            //ask the network what move to make given this game state
            float[] output = computerPlayer.FeedForward(gameState);
            //use one hot decoding to get the move
            int intMove = NNTools.OneHotDecode(output);

 
            bool valid = this.game.MakeMove(intMove, ref this.won, ref this.draw);
            if (valid)
            {
                this.lstBtnCells[intMove].Text = "O";
                this.EvalGame();
            }
            else
            {
                MessageBox.Show("Computer made an invalid move");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void EvalGame()
        {
            if (this.won) MessageBox.Show("Won");
            if (this.draw) MessageBox.Show("Draw");
        }

    
    }
}
