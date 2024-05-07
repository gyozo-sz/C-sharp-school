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
            return mtx.Cast<int>().Max();
        }

        static int FindConditionalMaxInMatrix(int[,] mtx, Predicate<int> condition)
        {
            return mtx.Cast<int>().Where(x => condition(x)).Max();
        }

        static int ConditionalCountELementsInMatrix(int[,] mtx, Predicate<int> condition)
        {
            return mtx.Cast<int>().Where(x => condition(x)).Count();
        }

        static void GenerateRandomMatrix(int minValue, int maxValue, int size, out int[,] mtx)
        {
            Random rnd = new Random();
            mtx = new int[size,size];

            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    mtx[i, j] = rnd.Next(minValue, maxValue);
                }
            }
        }

        static void ConditionalPrintMultidimensionalArray(int[,] mtx, Predicate<int> condition) {
            int c = 0;
            foreach (int x in mtx)
            {
                if (condition(x))
                {
                    Console.Write($"{x}\t");

                    c += 1;
                    if (c % 5 == 0)
                    {
                        Console.Write('\n');
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int minValue = -50;
            int maxValue = 50;
            int size = 8;

            GenerateRandomMatrix(minValue, maxValue, size, out int[,] mtx);

            ConditionalPrintMultidimensionalArray(mtx, IsOddAndPositive);

            Console.WriteLine($"\nNumber of elements fulfilling the conditions: {ConditionalCountELementsInMatrix(mtx, IsOddAndPositive)}");
            Console.WriteLine($"Maximum element of the restricted array: {FindConditionalMaxInMatrix(mtx, IsOddAndPositive)}");
            Console.WriteLine($"Maximum element of the array: {FindMaxInMatrix(mtx)}");
        }
    }
}