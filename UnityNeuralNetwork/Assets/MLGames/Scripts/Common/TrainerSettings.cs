﻿using static MLGames.NNSettings;

namespace TestPlatform.Common
{
    //TODO: Add worker server logic - the server logic.  the main server adds configurations to try.  the workers select configuration, run them and report the results
    //TODO: Add ML optimizer to the server to select which configuration to try next
    //TODO: Port non UI classes over to the Unity project

    /// <summary>
    /// 
    /// </summary>
    public class TrainerSettings
    {
        /// <summary>
        /// CSV string of layer sizes
        /// </summary>
        public string Layers { get; set; }
        /// <summary>
        /// CSV string of activation types
        /// </summary>
        public string Activations { get; set; }
        /// <summary>
        /// The probability of a mutation happening per parameter
        /// </summary>
        public float MutationChance { get; set; }
        /// <summary>
        /// The strength of a mutation
        /// </summary>
        public float MutionStrength { get; set; }
        /// <summary>
        /// Number of agents per generation
        /// </summary>
        public int PopulationSize { get; set; }
        /// <summary>
        /// Weight file containing the starting point
        /// </summary>
        public string WeightFileIn { get; set; }
        /// <summary>
        /// Base name of weight output files.  Output files will be written
        /// every N generations as specified by SaveFrequency.
        /// </summary>
        public string WeightFileOutBase { get; set; }
        /// <summary>
        /// Number of generations to pass before saving a weight file
        /// </summary>
        public int SaveWeightFileFrequency { get; set; }
        /// <summary>
        /// Forlder containing the training reports
        /// </summary>
        public string ReportFolder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WriteToReportFrequency { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int MaxGenerations { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int WinHistoryLength { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TrainerSettings()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int[] GetLayers()
        {
            return StringUtils.ParseIntArray(this.Layers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActivationFunctions[] GetActications()
        {
            return (ActivationFunctions[])(object)StringUtils.ParseIntArray(this.Activations);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetActivationNameList()
        {
            string strList = string.Empty;
            ActivationFunctions[] activations = this.GetActications();
            foreach(ActivationFunctions actFunction in activations)
            {
                strList += actFunction.ToString() + ",";
            }
            return strList;
        }
    }
}
