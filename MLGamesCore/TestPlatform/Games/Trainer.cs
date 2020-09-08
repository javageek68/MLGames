using MLGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MLGames.NNSettings;

namespace TestPlatform.Games
{
    /// <summary>
    /// Manages a population of games
    /// Uses Genetic Algorithm to manage networks
    /// Requests each network from GA and places it in a game
    /// 
    /// </summary>
    public class Trainer
    {
        public int[] layers = new int[3] { 9, 18, 9 };
        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.tanh, ActivationFunctions.tanh };

        public int populationSize = 100;
        public string WeightFile = string.Empty; // "TicTacToe.xml";

        public float MutationChance = 0.01f;

        public float MutationStrength = 0.5f;

        public bool GamesRunning = true;

        //stats
        public int ValidMoves = 0;
        public int InvalidMoves = 0;
        public int Wins = 0;
        public int Draws = 0;
        public int BadGames = 0;
        public int Generation = 0;

        private GeneticAlgorithm geneticAlgorithm = null;
        private List<TTTGame> games = new List<TTTGame>();

        public Trainer()
        {
            this.InitTraining();
        }

        private void InitTraining()
        {
            //create an instance of the genetic algorigthm
            this.geneticAlgorithm = new GeneticAlgorithm(this.layers, this.activation, this.populationSize, this.MutationChance, this.MutationStrength, this.WeightFile);
            this.geneticAlgorithm.Start();
            this.AssignGames();
        }

        private void AssignGames()
        {
            this.games = new List<TTTGame>();
            //loop through all of the NNs
            for (int i = 0; i < this.populationSize; i += 2)
            {
                //assign the NN to a game
                NeuralNetwork player1 = this.geneticAlgorithm.RequestNetwork(i);
                NeuralNetwork player2 = this.geneticAlgorithm.RequestNetwork(i + 1);
                TTTGame game = new TTTGame(player1, player2);
                this.games.Add(game);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PlayTurn()
        {
            bool blnFoundGameRunning = false;
            //calculate stats for this set of turn
            this.ValidMoves = 0;
            this.InvalidMoves = 0;
            this.Wins = 0;
            this.Draws = 0;
            this.BadGames = 0;

            //run each game
            foreach(TTTGame game in this.games)
            {
                if (game.status == TTTGame.GameStatus.running) 
                {
                    //if the game is still running, then call play
                    game.PlayTurn();

                    ////at least one game is running
                    blnFoundGameRunning = true;
                }
                else
                {
                    if (game.status == TTTGame.GameStatus.won) this.Wins++;
                    else if (game.status == TTTGame.GameStatus.draw) this.Draws++;
                    else if (game.status == TTTGame.GameStatus.badGame) this.BadGames++;
                }
                //calculate stats
                this.ValidMoves += game.ValidMoves;
                this.InvalidMoves += game.InvalidMoves;
            }
            this.GamesRunning = blnFoundGameRunning;
            if (!blnFoundGameRunning)
            {
                //all of the games have finished.

            }
            return blnFoundGameRunning;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reward()
        {
            this.geneticAlgorithm.EvolveNetworks();
            this.Generation++;
            this.AssignGames();
        }
    }
}
