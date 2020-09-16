using MLGames;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        public enum MessageTypes
        {
            info,
            waring,
            exception
        }

        public Action<MessageTypes, string> OnTrainerMessage { get; set; }

        public int[] layers = new int[3] { 9, 18, 9 };
        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.tanh, ActivationFunctions.tanh };

        public int populationSize = 100;
        public string WeightFileIn = string.Empty; // "TicTacToe.xml";
        public string WeightFileOut = string.Empty;
        public float MutationChance = 0.01f;
        public float MutationStrength = 0.5f;
        public int saveWeightsFrequency = -1;
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
        private NeuralNetwork startingNetwork;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="activation"></param>
        /// <param name="populationSize"></param>
        /// <param name="mutationChance"></param>
        /// <param name="mutationStrength"></param>
        /// <param name="strWeightFileIn"></param>
        /// <param name="strBaseWeightFileOut"></param>
        /// <param name="intSaveWeightsFrequency"></param>
        public Trainer(int[] layers, ActivationFunctions[] activation, int populationSize, float mutationChance, float mutationStrength, string strWeightFileIn, string strBaseWeightFileOut, int intSaveWeightsFrequency)
        {
            this.layers = layers;
            this.activation = activation;
            this.populationSize = populationSize;
            this.MutationChance = mutationChance;
            this.MutationStrength = mutationStrength;
            this.WeightFileIn = strWeightFileIn;
            this.WeightFileOut = strBaseWeightFileOut;
            this.saveWeightsFrequency = intSaveWeightsFrequency;
            if (strWeightFileIn.Trim().Length > 0) this.Load(strWeightFileIn);
            this.InitTraining();
        }

        private void InitTraining()
        {
            
            //create an instance of the genetic algorigthm
            this.geneticAlgorithm = new GeneticAlgorithm(this.layers, this.activation, this.populationSize, this.MutationChance, this.MutationStrength, this.startingNetwork);
            this.geneticAlgorithm.Start();
            this.AssignGames();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strFileName"></param>
        /// <returns></returns>
        public void Load(string strFileName)
        {
            string strContent = System.IO.File.ReadAllText(strFileName);
            NeuralNetwork nn = new NeuralNetwork(this.layers, this.activation);
            nn.Deserialize(strContent);
            this.startingNetwork = nn;
        }

        public void Save()
        {
            //get the output file name
            string strFileName = string.Format("{0}_{1:0000000}_{2:yyyyMMddhhmmss}.wght", this.WeightFileOut, this.Generation, DateTime.Now);
            //get the best network for ga
            NeuralNetwork nn = this.geneticAlgorithm.RequestBestNetwork();
            //serialize the network
            string strContents = nn.Serialize();
            //write it to a file
            System.IO.File.WriteAllText(strFileName, strContents);
        }

        /// <summary>
        /// 
        /// </summary>
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
            //save the weight files every saveWeightsFrequency generations
            if (this.Generation % this.saveWeightsFrequency == 0) this.Save();
            this.AssignGames();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="strMsg"></param>
        private void Log(MessageTypes messageType, string strMsg)
        {
            if (this.OnTrainerMessage != null) this.OnTrainerMessage.Invoke(messageType, strMsg);
        }
    }
}
