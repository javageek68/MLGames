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
    }
}
