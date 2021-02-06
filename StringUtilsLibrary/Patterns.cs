using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringUtilsLibrary
{
    public static class Patterns
    {
        /*
         * find patterns based on fixed size only, assuming ASCII str input
         * returns only patterns bigger than 1, or an empty list if none
         * 
         * Some inspirational material
         * https://docs.microsoft.com/en-us/archive/msdn-magazine/2018/january/csharp-all-about-span-exploring-a-new-net-mainstay
         * https://codereview.stackexchange.com/questions/194716/substring-vs-split-performance-test
         * https://www.stevejgordon.co.uk/an-introduction-to-optimising-code-using-span-t
         * https://www.meziantou.net/split-a-string-into-lines-without-allocation.htm
        */
        public static List<KeyValuePair<string, int>> GetPatternList(string inputString, int patternLength)
        {
            // basic validation:
            // input string should not be empty and should be ... ASCII?? let's check this =)
            // patternLength should be greater than 0 and less/equal than inputString.Length

            if (string.IsNullOrEmpty(inputString)) throw new ArgumentException("Could not be null or empty", "inputString");
            if (patternLength <= 0) throw new ArgumentException("Should be greater than 0", "patternLength");
            if (patternLength > inputString.Length) throw new ArgumentException("Should be less or equal than inputString.Length", "patternLength");

            var slicesDict = new Dictionary<string, int>();
            var resultList = new List<KeyValuePair<string, int>>();

            for (int i = 0; i < (inputString.Length - patternLength) + 1; i++)
            {
                // TODO: check - Do we need UTF8 support ?? 
                var inputStringASCII = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(inputString));
                var slice = new string(inputStringASCII.AsSpan(i, patternLength));

                if (slicesDict.TryGetValue(slice, out int patternCount))
                {
                    slicesDict[slice] = patternCount + 1;
                }
                else
                {
                    slicesDict.Add(slice, 1);
                }
            }

            resultList = slicesDict.Where(p => p.Value > 1).ToList();
            return resultList;
        }
    }
}
