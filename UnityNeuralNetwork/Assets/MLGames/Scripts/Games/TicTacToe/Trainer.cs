using MLGames;
using System;
using System.Collections.Generic;
using TestPlatform.Common;

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

        public TrainerSettings settings { get; set; }



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
        private List<int> WinsHistory = new List<int>();
        


        /// <summary>
        /// 
        /// </summary>
        /// <param name="trainerSettings"></param>
        public Trainer(TrainerSettings trainerSettings)
        {
            this.settings = trainerSettings;
            if (this.settings.WeightFileIn.Trim().Length > 0) this.Load(this.settings.WeightFileIn);
            this.InitTraining();
        }

        private void InitTraining()
        {

            //create an instance of the genetic algorigthm
            this.geneticAlgorithm = new GeneticAlgorithm(this.settings.GetLayers(), this.settings.GetActications(), this.settings.PopulationSize, this.settings.MutationChance, this.settings.MutionStrength, this.startingNetwork);
            //this.geneticAlgorithm = new GeneticAlgorithm(this.layers, this.activation, this.populationSize, this.MutationChance, this.MutationStrength, this.startingNetwork);
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
            NeuralNetwork nn = new NeuralNetwork(this.settings.GetLayers(), this.settings.GetActications());
            nn.Deserialize(strContent);
            this.startingNetwork = nn;
        }

        public void Save()
        {
            //get the output file name
            string strFileName = string.Format("{0}_{1:0000000}_{2:yyyyMMddhhmmss}.wght", this.settings.WeightFileOutBase, this.Generation, DateTime.Now);
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
            for (int i = 0; i < this.settings.PopulationSize; i += 2)
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
            //note if any games are still running
            this.GamesRunning = blnFoundGameRunning;
            if (!blnFoundGameRunning)
            {
                //all of the games have finished.
                //record history of wins
                this.WinsHistory.Add(this.Wins);
                //make sure we dont go over the limit
                if (this.WinsHistory.Count > this.settings.WinHistoryLength) this.WinsHistory.RemoveAt(0);

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
            if (this.Generation % this.settings.SaveWeightFileFrequency == 0) this.Save();
            this.AssignGames();
        }

        /// <summary>
        /// Check to see if training is over
        /// </summary>
        /// <returns></returns>
        public bool TrainingComplete()
        {
            bool trainingComplete = true;

            //check to see if we have reached the max number of generations
            bool reachedMaxGenerations = this.Generation > this.settings.MaxGenerations;
            //check to see if we have reached a plateau in training
            bool reachedPlateau = false;
            //see if we got better results WinHistoryLength generations ago
            if (this.Generation >= this.settings.WinHistoryLength)
            {
                //get the wins from the current generation
                int LastGen = this.WinsHistory[this.WinsHistory.Count-1];
                //get the wins from WinHistoryLength generations ago
                int FirstGen = this.WinsHistory[0];
                //if the old number of wins was greater, then training has reached its limits.
                reachedPlateau = FirstGen > LastGen;
            }

            //we are still training if we have not reached the max generations and have
            //not reached a training plateau
            trainingComplete = reachedMaxGenerations || reachedPlateau;

            return trainingComplete;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageType"></param>
        /// <param name="strMsg"></param>
        private void SendMessageToHost(MessageTypes messageType, string strMsg)
        {
            if (this.OnTrainerMessage != null) this.OnTrainerMessage.Invoke(messageType, strMsg);
        }
    }
}
