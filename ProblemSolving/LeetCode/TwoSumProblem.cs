using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving.LeetCode
{
    public class TwoSumProblem
    {
        public string LongestCommonPrefix(string[] strs)
        {
            string finalString = string.Empty;
            string comPareStr = string.Empty;
            for (int i = 0; i < strs.Length - 1; i++)
            {
                comPareStr = strs[i];
                if (strs[i] == comPareStr)
                {
                    finalString += comPareStr;
                }
            }

            return finalString;
        }

        public IEnumerable<int>ProduceEvenNumber(int upto)
        {
            for (int i = 0; i <= upto; i += 2)
            {
                yield return i;
            }
        }
    }
}


