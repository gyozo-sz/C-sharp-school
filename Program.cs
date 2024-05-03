namespace MyApp
{
    class ConsoleManager
    { 
        private int _lineNum;

        public ConsoleManager(int lineNum)
        {
            _lineNum = lineNum - 1;
        }

        private void WriteToConsole(string str)
        {
            Console.Write(str);
            ++_lineNum;
        }

        public string ReadStringFromConsole(string prompt)
        {
            string value;
            WriteToConsole(prompt);
            while (string.IsNullOrEmpty(value = Console.ReadLine()))
            {
                WriteToConsole("Please enter a value.\n");
                WriteToConsole(prompt);
            }
            return value;
        }

        public int ReadIntFromConsole(string prompt)
        {
            int value;
            WriteToConsole(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                WriteToConsole("Please enter a valid number.\n");
                WriteToConsole(prompt);
            }
            return value;
        }

        private bool IsEnteredPasswordValid(string password, int expectedLength)
        {
            if ( password.Length == expectedLength && int.TryParse(password, out int value))
            {
                return true;
            }

            return false;
        }

        public string ReadPasswordFromConsole(string prompt, int length) {
            string passwordPlaceholder = new string('*', length);
            string promptWithPlaceholders = prompt + passwordPlaceholder;

            WriteToConsole(promptWithPlaceholders);
            Console.SetCursorPosition(prompt.Length, _lineNum);

            string password = Console.ReadLine();
            while (!IsEnteredPasswordValid(password, length))
            {
                WriteToConsole("Please enter a valid password.\n");
                WriteToConsole(promptWithPlaceholders);
                Console.SetCursorPosition(prompt.Length, _lineNum);
                password = Console.ReadLine();
            }

            return password;
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public void PrettyPrintUserInfo(string name, int age, string email, string password)
        {
            Console.Write("User Information:\n");
            Console.Write("******************************************************\n");
            Console.Write($"\tName: {name}\n");
            Console.Write($"\tAge: {age}\n");
            Console.Write($"\tEmail: {email}\n");
            Console.Write($"\tPassword: {password}\n");
            Console.Write("******************************************************\n");
            Console.Write($"Name length is {name.Length}\n");
        }
    }

    internal class Program
    {

       
        
        static void Main(string[] args)
        {
            Console.Write("Please provide the following information:\n");
            ConsoleManager cm = new ConsoleManager(1);

            string name = cm.ReadStringFromConsole("Name: ");
            int age = cm.ReadIntFromConsole("Age: ");
            string email = cm.ReadStringFromConsole("Email: ");
            string password = cm.ReadPasswordFromConsole("Password: ", 4);

            cm.ClearConsole();

            cm.PrettyPrintUserInfo(name, age, email, password);

        }
    }
}