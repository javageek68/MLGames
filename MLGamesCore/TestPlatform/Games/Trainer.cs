using MLGames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MLGames.NNSettings;

namespace TestPlatform.Games
{
    public class Trainer
    {
        public int[] layers = new int[3] { 5, 3, 2 };
        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.tanh, ActivationFunctions.tanh };

        public int populationSize;
        public string WeightFile = "TicTacToe.xml";

        public float MutationChance = 0.01f;

        public float MutationStrength = 0.5f;

        public bool GamesRunning = true;

        private GeneticAlgorithm geneticAlgorithm = null;
        private List<TTTGame> games = new List<TTTGame>();

        private void InitTraining()
        {
            //create an instance of the genetic algorigthm
            this.geneticAlgorithm = new GeneticAlgorithm(this.layers, this.activation, this.populationSize, this.MutationChance, this.MutationStrength, this.WeightFile);
        }

        private void AssignGames()
        {
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
            foreach(TTTGame game in this.games)
            {
                if (game.running) 
                {
                    game.Play();
                    //at least one game is running
                    blnFoundGameRunning = true;
                }
            }
            this.GamesRunning = blnFoundGameRunning;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Reward()
        {
            this.geneticAlgorithm.EvolveNetworks();
        }
    }
}
