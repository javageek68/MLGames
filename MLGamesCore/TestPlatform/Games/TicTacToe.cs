using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
namespace TestPlatform.Games
{
    public class TicTacToe
    {
        public enum Turn
        {
            x,
            o
        }

        /// <summary>
        ///  0 - square is empty
        ///  1 - X
        /// -1 - O
        /// </summary>
        public int[] GameState { get; set; }

        private Turn currentTurn = Turn.x;

        /// <summary>
        /// 
        /// </summary>
        public TicTacToe()
        {
            this.GameState = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }

  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <param name="blnPlayerWon"></param>
        /// <param name="blnDraw"></param>
        /// <returns></returns>
        public bool MakeMove(int idx, ref bool blnPlayerWon, ref bool blnDraw)
        {
            bool blnValid = true;
            
            
            //if the square is already filled, then the move is invalid
            if (this.GameState[idx] != 0) return false;
            //default player number of X
            int playerNumber = 1;
            //if it is Os turn then set the player number for O
            if (this.currentTurn == Turn.o) playerNumber = -1;
            //make the move
            this.GameState[idx] = playerNumber;
            //check to see if the player won
            blnPlayerWon = this.PlayerWon(playerNumber);
            //check to see if there are any moves remaining
            bool blnMovesRemaining = this.MovesRemaining();
            //if the player didn't win and there are no more moves, then it is a draw
            blnDraw = (blnMovesRemaining == false && blnPlayerWon == false);

            //toggle the player
            if (this.currentTurn == Turn.o) this.currentTurn = Turn.x;
            else if (this.currentTurn == Turn.x) this.currentTurn = Turn.o;

            return blnValid;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkPlayer"></param>
        /// <returns></returns>
        private bool PlayerWon(int checkPlayer)
        {
            bool blnWon = false;
            //win conditions
            List<int[]> winConditions = new List<int[]>();
            //rows
            winConditions.Add(new int[3] { 0, 1, 2 });
            winConditions.Add(new int[3] { 3, 4, 5 });
            winConditions.Add(new int[3] { 6, 7, 8 });
            //cols
            winConditions.Add(new int[3] { 0, 3, 6 });
            winConditions.Add(new int[3] { 1, 4, 7 });
            winConditions.Add(new int[3] { 2, 5, 8 });
            //diags
            winConditions.Add(new int[3] { 0, 4, 8 });
            winConditions.Add(new int[3] { 2, 4, 6 });

            //loop through all winning conditions
            foreach(int[] cond in winConditions)
            {
                //check to see if this player has 3 items in a row
                bool gotThree = true;
                //loop through the index values of the condition
                foreach(int idxVal in cond)
                {
                    //if any of these are not the player then this condition didnt win
                    if (checkPlayer != this.GameState[idxVal]) gotThree = false; 
                }
                if (gotThree)
                {
                    //this player has won
                    blnWon = true;
                    break;
                }
            }
            return blnWon;
        }

        /// <summary>
        /// check to see if there are any 0s remaining on the board
        /// </summary>
        /// <returns></returns>
        private bool MovesRemaining()
        {
            bool blnRetVal = false;
            for(int i = 0; i < this.GameState.Length;i++)
            {
                if (this.GameState[i] == 0) blnRetVal = true;
            }
            return blnRetVal;    
        }

        public override string ToString()
        {
            int[] b = this.GameState;
            string strRetVal = string.Format("{0}|{1}|{2}\r\n----------\r\n{3}|{4}|{5}\r\n----------\r\n{6}|{7}|{8}\r\n", b[0], b[1], b[2], b[3], b[4], b[5], b[6], b[7], b[8]);
            strRetVal = strRetVal.Replace("1", "X").Replace("-1", "O").Replace("0", "   ");
            return strRetVal;
        }

    }
}
