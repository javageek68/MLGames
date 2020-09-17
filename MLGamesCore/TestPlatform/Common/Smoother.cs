using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TestPlatform.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class Smoother
    {
        private Dictionary<string, Queue<float>> dctValues = new Dictionary<string, Queue<float>>();
        private int queueSize = 10;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="intQueueSize"></param>
        public Smoother(int intQueueSize)
        {
            this.queueSize = intQueueSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public float GetValue(string key, float value)
        {
            float fltMean = 0f;
            if (this.dctValues.ContainsKey(key))
            {
                //get the queue for this variable
                Queue<float> queValues = this.dctValues[key];
                //add new value to the queue
                queValues.Enqueue(value);
                //make sure the queue doesn't go over the max queue size
                if (queValues.Count >= this.queueSize) queValues.Dequeue();
                float fltTotal = 0;
                foreach(float fltVal in queValues)
                {
                    fltTotal += fltVal;
                }
                fltMean = fltTotal / queValues.Count;
            }
            else
            {
                //this is a new value
                //add the new queue
                Queue<float> queValues = new Queue<float>();
                //enqueue the first value
                queValues.Enqueue(value);
                //add the variable to te dictionary
                this.dctValues.Add(key, queValues);
                //set the mean to the only value
                fltMean = value;
            }

            return fltMean;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int GetValue(string key, int value)
        {
            float fltValue = value;
            float fltReturnValue = this.GetValue(key, fltValue);
            return (int)fltReturnValue;
        }


    }
}
