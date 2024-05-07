namespace MyApp
{
    class ConsoleManager
    { 
        public string ReadStringFromConsole(string prompt)
        {
            string value;
            do
            {

                Console.Write(prompt);
                value = Console.ReadLine();
            }
            while(string.IsNullOrEmpty(value));
            return value;
        }

        public int ReadIntFromConsole(string prompt)
        {
            int value;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out value))
            {
                Console.Write("Please enter a valid number.\n");
                Console.Write(prompt);
            }
            return value;
        }

        private bool IsEnteredPasswordValid(string password, int expectedLength)
        {
            return password.Length == expectedLength && int.TryParse(password, out _);
        }

        public string ReadPasswordFromConsole(string prompt, int length) {
            string passwordPlaceholder = new string('*', length);
            string promptWithPlaceholders = prompt + passwordPlaceholder;

            Console.Write(promptWithPlaceholders);
            Console.SetCursorPosition(prompt.Length, Console.GetCursorPosition().Top);

            string password = Console.ReadLine();
            while (!IsEnteredPasswordValid(password, length))
            {
                Console.Write("Please enter a valid password.\n");
                Console.Write(promptWithPlaceholders);
                Console.SetCursorPosition(prompt.Length, Console.GetCursorPosition().Top);
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
            ConsoleManager cm = new ConsoleManager();

            string name = cm.ReadStringFromConsole("Name: ");
            int age = cm.ReadIntFromConsole("Age: ");
            string email = cm.ReadStringFromConsole("Email: ");
            string password = cm.ReadPasswordFromConsole("Password: ", 4);

            cm.ClearConsole();

            cm.PrettyPrintUserInfo(name, age, email, password);
        }
    }
}