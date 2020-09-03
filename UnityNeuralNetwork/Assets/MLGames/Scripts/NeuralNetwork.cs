using System.Collections.Generic;
using System;
using System.IO;
using static MLGames.NNSettings;

namespace MLGames
{
    /// <summary>
    /// 
    /// </summary>
    public class NeuralNetwork : IComparable<NeuralNetwork>
    {
 
        /// <summary>
        /// 
        /// </summary>
        public enum MessageTypes
        {
            Debug,
            Exception
        }

        public Action<MessageTypes, string> SendMessage { get; set; }

        public NNSettings settings = null;

        //genetic
        public float fitness = 0;//fitness

        //backprop
        public float learningRate = 0.01f;//learning rate
        public float cost = 0;


        private Random rnd = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="layerActivations"></param>
        public NeuralNetwork(int[] layers, ActivationFunctions[] layerActivations)
        {
            this.settings = new NNSettings(layers, layerActivations);
            this.rnd = new Random(DateTime.Now.Millisecond);
        
            InitNeurons();
            InitBiases();
            InitWeights();
        }

        #region "Init Network"
        /// <summary>
        /// create empty storage array for the neurons in the network.
        /// </summary>
        private void InitNeurons()
        {
            int[] layers = this.settings.layers;

            List<float[]> neuronsList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                neuronsList.Add(new float[layers[i]]);
            }
            this.settings.neurons = neuronsList.ToArray();
        }

        /// <summary>
        /// initializes random array for the biases being held within the network.
        /// </summary>
        private void InitBiases()
        {
            int[] layers = this.settings.layers;

            List<float[]> biasList = new List<float[]>();
            for (int i = 1; i < layers.Length; i++)
            {
                float[] bias = new float[layers[i]];
                for (int j = 0; j < layers[i]; j++)
                {
                    bias[j] = this.GetNextRandomFloat(-0.5f, 0.5f);
                }
                biasList.Add(bias);
            }
            this.settings.biases = biasList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private float GetNextRandomFloat(float min, float max)
        {
            //get the range of values
            float valRange = max - min;
            //scale and transform the value
            float randomValue = (float)rnd.NextDouble() * valRange + min;
            return randomValue;
        }

        /// <summary>
        /// initializes random array for the weights being held in the network.
        /// </summary>
        private void InitWeights()
        {
            int[] layers = this.settings.layers;

            List<float[][]> weightsList = new List<float[][]>();
            for (int i = 1; i < layers.Length; i++)
            {
                List<float[]> layerWeightsList = new List<float[]>();
                int neuronsInPreviousLayer = layers[i - 1];
                for (int j = 0; j < layers[i]; j++)
                {
                    float[] neuronWeights = new float[neuronsInPreviousLayer];
                    for (int k = 0; k < neuronsInPreviousLayer; k++)
                    {
                        neuronWeights[k] = this.GetNextRandomFloat(-0.5f, 0.5f);
                    }
                    layerWeightsList.Add(neuronWeights);
                }
                weightsList.Add(layerWeightsList.ToArray());
            }
            this.settings.weights = weightsList.ToArray();
        }
        #endregion

        /// <summary>
        /// feed forward, inputs >==> outputs.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float[] FeedForward(float[] inputs)
        {
            int[] layers = this.settings.layers;
            float[][] neurons = this.settings.neurons;
            float[][][] weights = this.settings.weights;
            float[][] biases = this.settings.biases;

            for (int i = 0; i < inputs.Length; i++)
            {
                neurons[0][i] = inputs[i];
            }
            for (int i = 1; i < layers.Length; i++)
            {
                int layer = i - 1;
                for (int j = 0; j < layers[i]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < layers[i - 1]; k++)
                    {
                        value += weights[i - 1][j][k] * neurons[i - 1][k];
                    }
                    neurons[i][j] = activate(value + biases[i - 1][j], layer);
                }
            }
            return neurons[layers.Length - 1];
        }
        
        /// <summary>
        /// call the activation function
        /// </summary>
        /// <param name="value"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public float activate(float value, int layer)
        {
            ActivationFunctions acivation = this.settings.layerActivations[layer];

            if (acivation == ActivationFunctions.sigmoid) return this.sigmoid(value);
            else if (acivation == ActivationFunctions.tanh) return this.tanh(value);
            else if (acivation == ActivationFunctions.relu) return this.relu(value);
            else if (acivation == ActivationFunctions.leakyrelu) return this.leakyrelu(value);
            else return this.relu(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public float activateDer(float value, int layer)//all activation function derivatives
        {
            ActivationFunctions acivation = this.settings.layerActivations[layer];
            if (acivation == ActivationFunctions.sigmoid) return this.sigmoidDer(value);
            else if (acivation == ActivationFunctions.tanh) return this.tanhDer(value);
            else if (acivation == ActivationFunctions.relu) return this.reluDer(value);
            else if (acivation == ActivationFunctions.leakyrelu) return this.leakyreluDer(value);
            else return this.reluDer(value);
        }

        #region "Activation functions and their derivatives"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float sigmoid(float x)//activation functions and their corrosponding derivatives
        {
            float k = (float)Math.Exp(x);
            return k / (1.0f + k);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float tanh(float x)
        {
            return (float)Math.Tanh(x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float relu(float x)
        {
            return (0 >= x) ? 0 : x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float leakyrelu(float x)
        {
            return (0 >= x) ? 0.01f * x : x;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float sigmoidDer(float x)
        {
            return x * (1 - x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float tanhDer(float x)
        {
            return 1 - (x * x);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float reluDer(float x)
        {
            return (0 >= x) ? 0 : 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public float leakyreluDer(float x)
        {
            return (0 >= x) ? 0.01f : 1;
        }
        #endregion

        public void Fit(List<float[]> X, List<float[]> y, int epochs)
        {
            for(int intCurruntEpoch = 0; intCurruntEpoch < epochs; intCurruntEpoch++)
            {
                int i = 0;
                foreach (float[] x in X)
                {
                    float[] expected = y[i];
                    BackPropagate(x, expected);
                    i++;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputs"></param>
        /// <param name="expected"></param>
        public void BackPropagate(float[] inputs, float[] expected)
        {
            int[] layers = this.settings.layers;
            float[][] neurons = this.settings.neurons;
            float[][][] weights = this.settings.weights;
            float[][] biases = this.settings.biases;


            float[] output = FeedForward(inputs);//runs feed forward to ensure neurons are populated correctly

            cost = 0;
            for (int i = 0; i < output.Length; i++) cost += (float)Math.Pow(output[i] - expected[i], 2);//calculated cost of network
            cost = cost / 2;//this value is not used in calculions, rather used to identify the performance of the network

            float[][] gamma;


            List<float[]> gammaList = new List<float[]>();
            for (int i = 0; i < layers.Length; i++)
            {
                gammaList.Add(new float[layers[i]]);
            }
            gamma = gammaList.ToArray();//gamma initialization

            int layer = layers.Length - 2;
            for (int i = 0; i < output.Length; i++) gamma[layers.Length - 1][i] = (output[i] - expected[i]) * activateDer(output[i], layer);//Gamma calculation
            for (int i = 0; i < layers[layers.Length - 1]; i++)//calculates the w' and b' for the last layer in the network
            {
                biases[layers.Length - 2][i] -= gamma[layers.Length - 1][i] * learningRate;
                for (int j = 0; j < layers[layers.Length - 2]; j++)
                {

                    weights[layers.Length - 2][i][j] -= gamma[layers.Length - 1][i] * neurons[layers.Length - 2][j] * learningRate;//*learning 
                }
            }

            for (int i = layers.Length - 2; i > 0; i--)//runs on all hidden layers
            {
                layer = i - 1;
                for (int j = 0; j < layers[i]; j++)//outputs
                {
                    gamma[i][j] = 0;
                    for (int k = 0; k < gamma[i + 1].Length; k++)
                    {
                        gamma[i][j] = gamma[i + 1][k] * weights[i][k][j];
                    }
                    gamma[i][j] *= activateDer(neurons[i][j], layer);//calculate gamma
                }
                for (int j = 0; j < layers[i]; j++)//itterate over outputs of layer
                {
                    biases[i - 1][j] -= gamma[i][j] * learningRate;//modify biases of network
                    for (int k = 0; k < layers[i - 1]; k++)//itterate over inputs to layer
                    {
                        weights[i - 1][j][k] -= gamma[i][j] * neurons[i - 1][k] * learningRate;//modify weights of network
                    }
                }
            }
        }


        /// <summary>
        /// used as a simple mutation function for any genetic implementations.
        /// </summary>
        /// <param name="high"></param>
        /// <param name="val"></param>
        public void Mutate(int high, float val)
        {
            float[][][] weights = this.settings.weights;
            float[][] biases = this.settings.biases;
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    biases[i][j] = (this.GetNextRandomFloat(0f, high) <= 2) ? biases[i][j] += this.GetNextRandomFloat(-val, val) : biases[i][j];
                }
            }

            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = (this.GetNextRandomFloat(0f, high) <= 2) ? weights[i][j][k] += this.GetNextRandomFloat(-val, val) : weights[i][j][k];
                    }
                }
            }
        }

        /// <summary>
        /// Comparing For Genetic implementations. Used for sorting based on the fitness of the network
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(NeuralNetwork other)
        {
            if (other == null) return 1;

            if (fitness > other.fitness)
                return 1;
            else if (fitness < other.fitness)
                return -1;
            else
                return 0;
        }

        /// <summary>
        /// For creatinga deep copy, to ensure arrays are serialzed.
        /// </summary>
        /// <param name="nn"></param>
        /// <returns></returns>
        public NeuralNetwork copy(NeuralNetwork nn) 
        {
            int[] layers = this.settings.layers;
            float[][] neurons = this.settings.neurons;
            float[][][] weights = this.settings.weights;
            float[][] biases = this.settings.biases;
            for (int i = 0; i < biases.Length; i++)
            {
                for (int j = 0; j < biases[i].Length; j++)
                {
                    nn.settings.biases[i][j] = biases[i][j];
                }
            }
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        nn.settings.weights[i][j][k] = weights[i][j][k];
                    }
                }
            }
            return nn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public void Load(string filename)
        {
            try
            {
                string strContent = File.ReadAllText(filename);
                this.Deserialize(strContent);
            }
            catch (Exception ex)
            {
                Log(MessageTypes.Exception, string.Format("Error loading weight file {0}", ex.ToString()));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNNContents"></param>
        public void Deserialize(string strNNContents)
        {
            try
            {
                this.settings = SerializerTools.DeserializeItem<NNSettings>(strNNContents);
            }
            catch (Exception ex)
            {
                Log(MessageTypes.Exception, string.Format("Error loading weight data {0}", ex.ToString()));
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="strErrMsg"></param>
        /// <returns></returns>
        public void Save(string filename)
        {
            try
            {
                string strContent = SerializerTools.SerializeItem<NNSettings>(this.settings);
                File.WriteAllText(filename, strContent);
            }
            catch (Exception ex)
            {
                Log(MessageTypes.Exception, string.Format("Error saving weight file {0}", ex.ToString()));
            }
        }

        /// <summary>
        /// Sends messages to the client object
        /// </summary>
        /// <param name="strMsg"></param>
        private void Log(MessageTypes messageType, string strMsg)
        {
            if (this.SendMessage != null) this.SendMessage(messageType, strMsg);
        }
    }
}