using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGames
{
    public class NNTools
    {
        /// <summary>
        /// returns the index of the largest value
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int OneHotDecode(float[] values)
        {
            float fltLargestValue = float.MinValue;
            int intLargestValueIndex = -1;
            for(int i = 0; i < values.Length; i++)
            {
                if (values[i] > fltLargestValue )
                {
                    fltLargestValue = values[i];
                    intLargestValueIndex = i;
                }
            }
            return intLargestValueIndex;
        }

        /// <summary>
        /// returns an array of 0s and a 1 in the index position.
        /// </summary>
        /// <param name="intSize"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static float[] OneHotEncode(int intSize, int index)
        {
            float[] output = new float[intSize];
            output[index] = 1;
            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layers"></param>
        /// <returns></returns>
        public static string ComposeLayerList(int[] layers)
        {
            string strList = string.Empty;
            for(int i = 0; i < layers.Length; i++)
            {
                strList += layers[i].ToString();
                if (i < layers.Length) strList += ",";
            }
            return strList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activationFunction"></param>
        /// <returns></returns>
        public static string ComposeActivationList(int[] activationFunction)
        {
            string strList = string.Empty;
            for (int i = 0; i < activationFunction.Length; i++)
            {
                strList += activationFunction[i].ToString();
                if (i < activationFunction.Length) strList += ",";
            }
            return strList;
        }

        /// <summary>
        /// replace each occurance of source in data with replaceValue
        /// </summary>
        /// <param name="data"></param>
        /// <param name="source"></param>
        /// <param name="replaceValue"></param>
        public static void Transform(ref float[] data, float source, float replaceValue)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == source) data[i] = replaceValue;
            }
        }
    }
}
