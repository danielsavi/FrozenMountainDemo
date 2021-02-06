using System;
using System.Collections.Generic;
using StringUtilsLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<KeyValuePair<string, int>> patternList = Patterns.GetPatternList("zf3kabxcde224lkzf3mabxc51+crsdtzf3nab=", 3);
            foreach (var p in patternList)
            {
                Console.WriteLine($"{p.Key} {p.Value}");
            }
            
            Console.ReadKey();
        }
    }
}
