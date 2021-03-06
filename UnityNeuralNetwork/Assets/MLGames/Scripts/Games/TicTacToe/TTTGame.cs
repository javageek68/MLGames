﻿using MLGames;
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
        public enum GameStatus
        {
            running,
            won,
            draw,
            badGame
        }
        public NeuralNetwork playerX { get; set; }
        public NeuralNetwork playerO { get; set; }

        public TicTacToe game { get; set; }

        public GameStatus status { get; set; }

        public int ValidMoves = 0;
        public int InvalidMoves = 0;

      
        private float InvalidMoveScore = -1f;
        private float ValidMoveScore = 0.1f;
        private float WonScore = 1f;
        private float LostScore = -1f;

        private float current = 1;
        private float other = 0.5f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public TTTGame(NeuralNetwork player1, NeuralNetwork player2)
        {
            this.status = GameStatus.running;
            this.playerX = player1;
            this.playerO = player2;
            this.game = new TicTacToe();
        }

        /// <summary>
        /// 
        /// </summary>
        public void PlayTurn()
        {
  
            bool valid = false;
     
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
                NNTools.Transform(ref gameState, this.game.X, this.current);
                NNTools.Transform(ref gameState, this.game.O, this.other);
                currentPlayer = this.playerX;
                otherPlayer = this.playerO;
            }
            else
            {
                //O is the current player
                NNTools.Transform(ref gameState, this.game.X, this.other);
                NNTools.Transform(ref gameState, this.game.O, this.current);
                currentPlayer = this.playerO;
                otherPlayer = this.playerX;
            }

            //ask the network what move to make given this game state
            float[] output = currentPlayer.FeedForward(gameState);
            int idx = NNTools.OneHotDecode(output);

            bool won = false;
            bool draw = false;
            valid = this.game.MakeMove(idx, ref won, ref draw);
            
            //set the game status
            if (won) this.status = GameStatus.won;
            if (draw) this.status = GameStatus.draw;
            if (!valid) this.status = GameStatus.badGame;

            
            
            if (valid)
            {
                //we made a valid move
                //update stats
                this.ValidMoves++;
                //update player fitness
                currentPlayer.fitness += this.ValidMoveScore;
                //check to see if we won
                if (this.status == GameStatus.won) 
                { 
                    //reward the winner and loser
                    currentPlayer.fitness += this.WonScore;
                    otherPlayer.fitness += this.LostScore;
                }
            }
            else
            {
                //upate stats
                this.InvalidMoves++;
                //update player fitness
                currentPlayer.fitness += this.InvalidMoveScore;
                //end the game if a player makes an invalid move
            }
        }

    }
}
