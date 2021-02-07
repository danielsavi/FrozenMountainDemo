using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace StringUtilsLibrary
{
    public static class Patterns
    {
        /*
         * find patterns based on a fixed size only window
         * returns only patterns bigger than 1, or an empty list if none
         * 
         * if inputStr is coming from the wire/internet, we should check if its well-formed, https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-encoding-introduction
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
            // input string should not be empty
            // patternLength should be greater than 0 and less/equal than inputString.Length

            if (string.IsNullOrEmpty(inputString)) throw new ArgumentException("Could not be null or empty", "inputString");
            if (patternLength <= 0) throw new ArgumentException("Should be greater than 0", "patternLength");
            if (patternLength > inputString.Length) throw new ArgumentException("Should be less or equal than inputString.Length", "patternLength");

            var slicesDict = new Dictionary<string, int>();
            var resultList = new List<KeyValuePair<string, int>>();
            var inputStringInfo = new StringInfo(inputString); //Dirty-fix to UTF8 strings

            for (int i = 0; i < (inputStringInfo.LengthInTextElements - patternLength) + 1; i++)
            {
                var slice = inputStringInfo.SubstringByTextElements(i, patternLength);
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
