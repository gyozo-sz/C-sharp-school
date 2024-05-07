﻿namespace MyApp
{
    class Calculator
    { 
        private enum CalculatorOperation
        {
            Addition = 1, 
            Subtraction = 2, 
            Multiplication = 3, 
            Division = 4, 
            Exit = 5
        }

        static public int ReadIntFromConsole(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Please enter a valid number.");
                Console.Write(prompt);
            }
            return value;
        }

        static private CalculatorOperation ReadOperatorCodeFromConsole(string prompt)
        {
            while (true)
            {
                int operatorCode = ReadIntFromConsole(prompt);
                if (!IsOperatorCodeValid(operatorCode))
                {
                    Console.WriteLine("Selected option is invalid. Please select a valid option.");
                } else
                {
                    return (CalculatorOperation)operatorCode;
                }
            }
        }

        static private bool IsOperatorCodeValid(int operatorCode)
        {
            return operatorCode > 0 && operatorCode < 6;
        }

        static public void PrintActionMenu()
        {
            Console.WriteLine("Please select the operation you wish to perform:");
            Console.WriteLine("1 - Addition");
            Console.WriteLine("2 - Substraction");
            Console.WriteLine("3 - Multiplication");
            Console.WriteLine("4 - Division");
            Console.WriteLine("5 - Exit");
        }

        static public void ClearConsole()
        {
            Console.Clear();
        }

        static private void PerformAction(CalculatorOperation operation, int argumentOne, int argumentTwo)
        {
            switch (operation)
            {
                case CalculatorOperation.Addition:
                    Console.WriteLine($"The result of the addition is {argumentOne + argumentTwo}");
                    break;
                case CalculatorOperation.Subtraction:
                    Console.WriteLine($"The result of the substraction is {argumentOne - argumentTwo}");
                    break;
                case CalculatorOperation.Multiplication:
                    Console.WriteLine($"The result of the multiplication is {argumentOne * argumentTwo}");
                    break;
                case CalculatorOperation.Division:
                    if (argumentTwo == 0)
                    {
                        Console.WriteLine("Can not perform division by zero.");
                        break;
                    }
                    double convertedArgOne = Convert.ToDouble(argumentOne);
                    double convertedArgTwo = Convert.ToDouble(argumentTwo);
                    Console.WriteLine($"The result of the division is {convertedArgOne / convertedArgTwo}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), "Invalid operation value is passed for execution");

            }
        }

        static private void WaitForUserInput()
        {
            Console.WriteLine("Press any key to return to the operation selection menu...");
            Console.ReadKey();
        }

        static public void Run()
        {
            while (true)
            {
                ClearConsole();
                PrintActionMenu();
                CalculatorOperation selectedOperatorCode = ReadOperatorCodeFromConsole("Enter your option: ");

                if (selectedOperatorCode == CalculatorOperation.Exit)
                {
                    Console.WriteLine("Exiting calculator app.");
                    return;
                }

                ClearConsole();

                int argumentOne = ReadIntFromConsole("Enter the first number: ");
                int argumentTwo = ReadIntFromConsole("Enter the second number: ");

                PerformAction(selectedOperatorCode, argumentOne, argumentTwo);
                WaitForUserInput();
            }
        }
    }

    internal class Program
    {
        static void Main()
        {
            Calculator.Run();
            

        }
    }
}