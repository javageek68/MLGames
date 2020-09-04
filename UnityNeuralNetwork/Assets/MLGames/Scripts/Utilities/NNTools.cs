using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLGames
{
    public class NNTools
    {
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
    }
}
