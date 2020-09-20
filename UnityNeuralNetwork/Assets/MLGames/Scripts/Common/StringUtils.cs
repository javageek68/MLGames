using System;
using System.Collections.Generic;

namespace TestPlatform.Common
{
    public class StringUtils
    {
   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strCSV"></param>
        /// <returns></returns>
        public static int[] ParseIntArray(string strCSV)
        {
   
            //normalize the string
            strCSV = strCSV.Replace("\r", string.Empty)
                                    .Replace("\n", string.Empty)
                                    .Replace(" ", string.Empty);
            //get the min max pairs
            string[] strValues = strCSV.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            List<int> lstInts = new List<int>();
            foreach(string strValue in strValues)
            {
                lstInts.Add(int.Parse(strValue));
            }

            return lstInts.ToArray();
        }
    }
}
