using MLGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatform.Games
{
    public class TTTGame
    {
        public NeuralNetwork playerX { get; set; }
        public NeuralNetwork playerO { get; set; }

        public TicTacToe game { get; set; }

        public bool running { get; set; }

        public int ValidMoves = 0;
        public int InvalidMoves = 0;
        public bool won = false;
        public bool draw = false;
       

        private float InvalidMoveScore = -1f;
        private float ValidMoveScore = 0.1f;
        private float WonScore = 1f;
        private float LostScore = -1f;
        private float DrawScore = 0;

        private float current = 1;
        private float other = 0.5f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public TTTGame(NeuralNetwork player1, NeuralNetwork player2)
        {
            this.running = true;
            this.playerX = player1;
            this.playerO = player2;
            this.game = new TicTacToe();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
  
            bool valid = false;
            float score = 0;

            //get a copy of the game state
            float[] gameState = new float[this.game.GameState.Length];
            this.game.GameState.CopyTo(gameState,0);
            NeuralNetwork currentPlayer = null;
            NeuralNetwork otherPlayer = null;

            //transform the gamestate to the player's perspective
            //game state - 10 is x -10 is O
            //networ     - 1 is current player 0.5 is other player
            if (this.game.currentTurn == TicTacToe.Turn.x)
            {
                //X is the current player
                this.Transform(ref gameState, this.game.X, this.current);
                this.Transform(ref gameState, this.game.O, this.other);
                currentPlayer = this.playerX;
                otherPlayer = this.playerO;
            }
            else
            {
                //O is the current player
                this.Transform(ref gameState, this.game.X, this.other);
                this.Transform(ref gameState, this.game.O, this.current);
                currentPlayer = this.playerO;
                otherPlayer = this.playerX;
            }

            //ask the network what move to make given this game state
            float[] output = currentPlayer.FeedForward(gameState);
            int idx = NNTools.OneHotDecode(output);
            
        
            valid = this.game.MakeMove(idx, ref this.won, ref this.draw);
            //grade for making an invalid move
            
            if (valid)
            {
                //we made a valid move
                //update stats
                this.ValidMoves++;
                //update player fitness
                currentPlayer.fitness += this.ValidMoveScore;
                //check to see if we won
                if (this.won) 
                { 
                    //reward the winner and loser
                    currentPlayer.fitness += this.WonScore;
                    otherPlayer.fitness += this.LostScore;
                    this.running = false;
                }

                if (this.draw) this.running = false;
            }
            else
            {
                //upate stats
                this.InvalidMoves++;
                //update player fitness
                currentPlayer.fitness += this.InvalidMoveScore;
                //end the game if a player makes an invalid move
                this.running = false;
            }
        }


        /// <summary>
        /// replace each occurance of source in data with replaceValue
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <param name="replaceValue"></param>
        private void Transform(ref float[] data, float source, float replaceValue)
        {
            for(int i =0;i<data.Length;i++)
            {
                if (data[i] == source) data[i] = replaceValue;
            }
        }
    }
}
