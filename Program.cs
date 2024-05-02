using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start of the World!");
            int i = 0;
            while (i < 5)
            {
                Console.WriteLine("Hello World!");
                ++i;
            }
            Console.WriteLine("End of the World!");
        }
    }
}