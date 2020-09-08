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

        public int populationSize = 4;
        public string WeightFile = "TicTacToe.xml";

        public float MutationChance = 0.01f;

        public float MutationStrength = 0.5f;

        public bool GamesRunning = true;

        //stats
        public int ValidMoves = 0;
        public int InvalidMoves = 0;
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
        public void Play()
        {
            bool blnFoundGameRunning = false;
            this.ValidMoves = 0;
            this.InvalidMoves = 0;
            foreach(TTTGame game in this.games)
            {
                if (game.running) 
                {
                    game.Play();
                    this.ValidMoves += game.ValidMoves;
                    this.InvalidMoves += game.InvalidMoves;
                    //at least one game is running
                    blnFoundGameRunning = true;
                }
            }
            this.GamesRunning = blnFoundGameRunning;
            if (!blnFoundGameRunning)
            {
                //all of the games have finished.
                //it's time to set assign rewards 
                this.Reward();
            }
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
