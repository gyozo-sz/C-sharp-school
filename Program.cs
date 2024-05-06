using System;

namespace MyApp
{


    internal class Program
    {
        private static void FillArrayFromConsoleInput(ref int[] array)
        {
            int value;
            for (int i = 0; i < array.Length; i++)
            {
                while(!int.TryParse(Console.ReadLine(), out value))
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                array[i] = value;
            }
        }

        private static void DisplayArrayElements(int[] array)
        {
            foreach(int x in array)
            {
                Console.Write($"{x} ");
            }
            Console.Write("\n");
        }

        private static int CompareDescending(int x, int y)
        {
            return y.CompareTo(x);
        }

        private static void SortArrayDescending(ref int[] array)
        {
            Array.Sort(array, CompareDescending);
        }

        private static void SortArrayDescendingOwn(ref int[] array)
        {
            for (int i = 0; i < array.Length; ++i)
            {
                for (int j = i+1; j < array.Length; ++j)
                {
                    if (array[i] < array[j])
                    {
                        (array[i], array[j]) = (array[j], array[i]);
                        //int temp = array[i];
                        //array[i] = array[j];
                        //array[j] = temp;
                    }
                }
            }
        }

        private static bool AreArraysEquivalent(int[] array1, int[] array2)
        {
            return array1.SequenceEqual(array2);
        }

        static void Main(string[] args)
        {
            int[] array1 = new int[5];
            int[] array2 = new int[5];

            Console.WriteLine("Please enter digits for the first array:");
            FillArrayFromConsoleInput(ref array1);
            Console.WriteLine("Please enter digits for the second array:");
            FillArrayFromConsoleInput(ref array2);

            Console.Write("\n\n");

            Console.WriteLine("First array elements before sorting:");
            DisplayArrayElements(array1);
            Console.WriteLine("Second array elements before sorting:");
            DisplayArrayElements(array2);

            Console.Write("\n");

            SortArrayDescendingOwn(ref array1);
            SortArrayDescendingOwn(ref array2);
            Console.WriteLine("First array elements after sorting:");
            DisplayArrayElements(array1);
            Console.WriteLine("Second array elements after sorting:");
            DisplayArrayElements(array2);

            bool eq = AreArraysEquivalent(array1, array2);
            Console.WriteLine("\nThe given arrays are {0}equivalent", eq ? "" : "NOT ");
        }
    }
}