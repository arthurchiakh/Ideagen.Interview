using System;
using System.Collections.Generic;

namespace Ideagen.Interview
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach (var arg in args)
                Console.WriteLine(Calculator.Calculate(arg));
        }
    }
}