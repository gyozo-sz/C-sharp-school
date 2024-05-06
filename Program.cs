using System;

namespace MyApp
{
    internal class Program
    {
        static bool IsOddAndPositive(int num)
        {
            return num % 2 == 1 && num > 0;
        }

        static int FindMaxInMatrix(int[,] mtx)
        {
            int max = int.MinValue;
            foreach(int x in mtx)
            {
                if (x > max)
                {
                    max = x;
                }
            }
            return max;
        }

        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[,] mtx = new int[8, 8];

            for (int i = 0; i < mtx.GetLength(0); ++i)
            {
                for (int j = 0; j < mtx.GetLength(1); ++j)
                {
                    mtx[i, j] = rnd.Next(-50, 50);
                }
            }

            int c = 0;
            int restrictedMax = int.MinValue;
            foreach (int x in mtx)
            {
                if (IsOddAndPositive(x)){
                    Console.Write($"{x}\t");
                    if (x > restrictedMax)
                    {
                        restrictedMax = x;
                    }

                    c += 1;
                    if (c % 5 == 0)
                    {
                        Console.Write('\n');
                    }
                }
            }

            Console.WriteLine($"\nNumber of elements fulfilling the conditions: {c}");
            Console.WriteLine($"Maximum element of the restricted array: {restrictedMax}");
            Console.WriteLine($"Maximum element of the array: {FindMaxInMatrix(mtx)}");
        }
    }
}