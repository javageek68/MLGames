using System.Collections;
using System.Collections.Generic;

using static MLGames.NeuralNetwork;
using static MLGames.NNSettings;

namespace MLGames
{
    /// <summary>
    /// Creates a population of networks
    /// Allows each network to be requested by a trainer
    /// Evolves the population of networks based on fitness
    /// </summary>
    public class GeneticAlgorithm 
    {

        public int[] layers = new int[3] { 5, 3, 2 };
        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.tanh , ActivationFunctions.tanh };

        public int populationSize;
        public string WeightFile = string.Empty; // "Assets/TicTacToe.txt";

        public float MutationChance = 0.01f;

        public float MutationStrength = 0.5f;


        public List<NeuralNetwork> networks;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="activation"></param>
        /// <param name="populationSize"></param>
        /// <param name="mutationChance"></param>
        /// <param name="mutationStrength"></param>
        /// <param name="weightFileName"></param>
        public GeneticAlgorithm(int[] layers, ActivationFunctions[] activation, int populationSize, float mutationChance, float mutationStrength, string weightFileName)
        {
            this.layers = layers;
            this.activation = activation;
            this.populationSize = populationSize;
            this.MutationChance = mutationChance;
            this.MutationStrength = mutationStrength;
            this.WeightFile = weightFileName;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {
            if (populationSize % 2 != 0)
                populationSize = 50;//if population size is not even, sets it to fifty

            InitNetworks();
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitNetworks()
        {

            networks = new List<NeuralNetwork>();
            string strContents = string.Empty;
            //load the weight file if it exists
            if (System.IO.File.Exists(this.WeightFile)) strContents = System.IO.File.ReadAllText(this.WeightFile);

            //create the initial population of networks
            for (int i = 0; i < populationSize; i++)
            {
                NeuralNetwork net = new NeuralNetwork(layers, activation);
                //if we loaded a network in, then deserialize it
                if (strContents.Trim().Length > 0) net.Deserialize(strContents);
                //add the new network to the population
                networks.Add(net);
            }
        }

 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="networkIndex"></param>
        /// <returns></returns>
        public NeuralNetwork RequestNetwork(int networkIndex)
        {
            NeuralNetwork nn = null;
            if (networkIndex < this.networks.Count  ) nn = this.networks[networkIndex];
            return nn;
        }

        /// <summary>
        /// 
        /// </summary>
        public void EvolveNetworks()
        {
            //sort the networks by fitness
            networks.Sort();
            //save the weights to a file if one was provided
            if (this.WeightFile.Trim().Length>0) networks[populationSize - 1].Save(this.WeightFile);
            //loop through the top half of the networks
            for (int i = 0; i < populationSize / 2; i++)
            {
                //replace the bottom half with a copy of the top half
                networks[i] = networks[i + populationSize / 2].copy(new NeuralNetwork(layers, activation));
                //perform random mutations
                networks[i].Mutate((int)(1 / MutationChance), MutationStrength);
            }
        }
    }
}
