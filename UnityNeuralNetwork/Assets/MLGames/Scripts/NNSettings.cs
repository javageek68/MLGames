using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGames
{ 
    public class NNSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public enum ActivationFunctions
        {
            sigmoid = 0,
            tanh = 1,
            relu = 2,
            leakyrelu = 3
        }

        /// <summary>
        /// 
        /// </summary>
        public int[] layers { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float[][] neurons { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float[][] biases { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float[][][] weights { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int[] activations { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ActivationFunctions[] layerActivations { get; set; }

        public NNSettings()
        {
           
        }

        public NNSettings(int[] layers, ActivationFunctions[] layerActivations)
        {
            this.layers = new int[layers.Length];
            for (int i = 0; i < layers.Length; i++)
            {
                this.layers[i] = layers[i];
            }

            this.layerActivations = layerActivations;

            activations = new int[layers.Length - 1];
            for (int i = 0; i < layers.Length - 1; i++)
            {
                activations[i] = (int)layerActivations[i];
            }
        }
    }
}
