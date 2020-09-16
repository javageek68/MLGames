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

            for (int intInputIdx = 0; intInputIdx < inputs.Length; intInputIdx++)
            {
                neurons[0][intInputIdx] = inputs[intInputIdx];
            }
            for (int intLayerIdx = 1; intLayerIdx < layers.Length; intLayerIdx++)
            {
                int layer = intLayerIdx - 1;
                for (int j = 0; j < layers[intLayerIdx]; j++)
                {
                    float value = 0f;
                    for (int k = 0; k < layers[intLayerIdx - 1]; k++)
                    {
                        value += weights[intLayerIdx - 1][j][k] * neurons[intLayerIdx - 1][k];
                    }
                    neurons[intLayerIdx][j] = activate(value + biases[intLayerIdx - 1][j], layer);
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

        public void Fit(List<float[]> X, List<float[]> Y, int epochs)
        {
            for(int intCurruntEpoch = 0; intCurruntEpoch < epochs; intCurruntEpoch++)
            {
                int i = 0;
                foreach (float[] x in X)
                {
                    float[] expected = Y[i];
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
            //calculated cost of network
            for (int i = 0; i < output.Length; i++) cost += (float)Math.Pow(output[i] - expected[i], 2);
            //performance cost
            cost = cost / 2;

            float[][] gamma;


            List<float[]> gammaList = new List<float[]>();
            for (int intLayerIdx = 0; intLayerIdx < layers.Length; intLayerIdx++)
            {
                gammaList.Add(new float[layers[intLayerIdx]]);
            }
            //gamma initialization
            gamma = gammaList.ToArray();

            int layer = layers.Length - 2;
            //Gamma calculation
            for (int i = 0; i < output.Length; i++) gamma[layers.Length - 1][i] = (output[i] - expected[i]) * activateDer(output[i], layer);
            //calculates the w' and b' for the last layer in the network
            for (int i = 0; i < layers[layers.Length - 1]; i++)
            {
                biases[layers.Length - 2][i] -= gamma[layers.Length - 1][i] * learningRate;
                for (int j = 0; j < layers[layers.Length - 2]; j++)
                {
                    //learning
                    weights[layers.Length - 2][i][j] -= gamma[layers.Length - 1][i] * neurons[layers.Length - 2][j] * learningRate; 
                }
            }

            //runs on all hidden layers
            for (int intInLayerIdx = layers.Length - 2; intInLayerIdx > 0; intInLayerIdx--)
            {
                layer = intInLayerIdx - 1;
                //outputs
                for (int j = 0; j < layers[intInLayerIdx]; j++)
                {
                    gamma[intInLayerIdx][j] = 0;
                    for (int k = 0; k < gamma[intInLayerIdx + 1].Length; k++)
                    {
                        gamma[intInLayerIdx][j] = gamma[intInLayerIdx + 1][k] * weights[intInLayerIdx][k][j];
                    }
                    //calculate gamma
                    gamma[intInLayerIdx][j] *= activateDer(neurons[intInLayerIdx][j], layer);
                }
                //itterate over outputs of layer
                for (int j = 0; j < layers[intInLayerIdx]; j++)
                {
                    //modify biases of network
                    biases[intInLayerIdx - 1][j] -= gamma[intInLayerIdx][j] * learningRate;
                    for (int k = 0; k < layers[intInLayerIdx - 1]; k++)//itterate over inputs to layer
                    {
                        //modify weights of network
                        weights[intInLayerIdx - 1][j][k] -= gamma[intInLayerIdx][j] * neurons[intInLayerIdx - 1][k] * learningRate;
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
        #region "Save and Load"
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

        public string Serialize()
        {
            return SerializerTools.SerializeItem<NNSettings>(this.settings);
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
                string strContent = this.Serialize();
                File.WriteAllText(filename, strContent);
            }
            catch (Exception ex)
            {
                Log(MessageTypes.Exception, string.Format("Error saving weight file {0}", ex.ToString()));
            }
        }
        #endregion

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