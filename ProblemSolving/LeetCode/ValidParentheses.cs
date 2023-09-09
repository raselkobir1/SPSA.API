using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemSolving.LeetCode
{
    public class ValidParentheses
    {
        public bool IsValid(string s)
        {

            var arry = s.ToCharArray();
            //if( arry.Count() / 2 != 0)
            //    return false;

            var x = arry.Count() / 2;

            string data = "(){}[]";
            for (int i = 0; i < data.Length -1; i++)
            {
                var isValid = data.Contains(arry[i]);
                if (!isValid)
                    return false;
            };

            return true;
        }
    }
}

