namespace MyApp
{
    class Calculator
    { 
        public int ReadIntFromConsole(string prompt)
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

        public int ReadOperatorCodeFromConsole(string prompt)
        {
            int operatorCode = -1;
            while (true)
            {
                operatorCode = ReadIntFromConsole(prompt);
                if (!IsOperatorCodeValid(operatorCode))
                {
                    Console.WriteLine("Selected option is invalid. Please select a valid option.");
                } else
                {
                    break;
                }
            }
            return operatorCode;
            
        }

        private bool IsOperatorCodeValid(int operatorCode)
        {
            return operatorCode > 0 && operatorCode < 6;
        }

        public void PrintActionMenu()
        {
            Console.WriteLine("Please select the operation you wish to perform:");
            Console.WriteLine("1 - Addition");
            Console.WriteLine("2 - Substraction");
            Console.WriteLine("3 - Multiplication");
            Console.WriteLine("4 - Division");
            Console.WriteLine("5 - Exit");
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void PerformAction(int operatorCode, int argumentOne, int argumentTwo)
        {
            switch (operatorCode)
            {
                case 1:
                    Console.WriteLine($"The result of the addition is {argumentOne + argumentTwo}");
                    break;
                case 2:
                    Console.WriteLine($"The result of the substraction is {argumentOne - argumentTwo}");
                    break;
                case 3:
                    Console.WriteLine($"The result of the multiplication is {argumentOne * argumentTwo}");
                    break;
                case 4:
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
                    throw new ArgumentOutOfRangeException();

            }
        }

        public void Run()
        {
            PrintActionMenu();
            int selectedOperatorCode = ReadOperatorCodeFromConsole("Enter your option: ");

            if (selectedOperatorCode == 5) {
                Console.WriteLine("Exiting calculator app.");
                return;
            }

            ClearConsole();

            int argumentOne = ReadIntFromConsole("Enter the first number: ");
            int argumentTwo = ReadIntFromConsole("Enter the second number: ");

            PerformAction(selectedOperatorCode, argumentOne, argumentTwo);
            
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Calculator c = new Calculator();
            c.Run();
            

        }
    }
}