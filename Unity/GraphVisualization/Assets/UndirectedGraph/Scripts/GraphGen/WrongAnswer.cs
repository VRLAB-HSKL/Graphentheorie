using System.Collections.Generic;
using UnityEngine;

namespace GraphGen
{
    /// <summary>
    /// Generates a wrong answer, i.e. a square matrix of the specified size is created. 
    /// </summary>
    public class WrongAnswer
    {
        private int size;

        public WrongAnswer(int size)
        {
            this.size = size;
        }

        /// <summary>
        /// returns a randomly generated either 0 or 1 as per given size of the Matrix
        /// </summary>
        /// <returns></returns>
        public List<List<int>> GetWAnswer()
        {
            List<List<int>> res = new List<List<int>>();
            for (int i = 0; i < size; i++)
            {
                var temp = new List<int>();
                for (int j = 0; j < size; j++)
                {
                    int val = Random.Range(0, 2);
                    temp.Add(val);
                }

                res.Add(temp);
            }

            return res;
        }
    }
}