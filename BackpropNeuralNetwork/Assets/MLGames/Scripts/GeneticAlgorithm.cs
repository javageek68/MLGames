using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MLGames.NeuralNetwork;

namespace MLGames
{
    public class GeneticAlgorithm : MonoBehaviour
    {

        public int[] layers = new int[3] { 5, 3, 2 };
        public ActivationFunctions[] activation = new ActivationFunctions[2] { ActivationFunctions.tanh , ActivationFunctions.tanh };

        public int populationSize;
        public string WeightFile = "Assets/Save.txt";

        [Range(0.0001f, 1f)] public float MutationChance = 0.01f;

        [Range(0f, 1f)] public float MutationStrength = 0.5f;


        public List<NeuralNetwork> networks;
        private List<Bot> cars;


        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            Debug.Log("entered start");
            if (populationSize % 2 != 0)
                populationSize = 50;//if population size is not even, sets it to fifty

            InitNetworks();
        }

        /// <summary>
        /// 
        /// </summary>
        public void InitNetworks()
        {
            networks = new List<NeuralNetwork>();

            for (int i = 0; i < populationSize; i++)
            {
                NeuralNetwork net = new NeuralNetwork(layers, activation);
                if (this.WeightFile.Trim().Length > 0)
                {
                    net.Load(this.WeightFile);
                }
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
            if (this.networks.Count < networkIndex) nn = this.networks[networkIndex];
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
