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
    }
}
