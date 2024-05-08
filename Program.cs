﻿namespace MyApp
{
    class User
    {
        private readonly string _userId;
        private readonly string _name;
        private uint _age;

        public User (string userId, string name, uint age)
        {
            _userId = userId;
            _name = name;
            _age = age;
        }

        public void PrintInfo()
        {
            Console.WriteLine("User Info:");
            Console.WriteLine($"\tID: {_userId}");
            Console.WriteLine($"\tName: {_name}");
            Console.WriteLine($"\tAge: {_age}");
        }

        public static void ChangeUserAge(User u, uint newAge)
        {
            u._age = newAge;
        }
    }
    internal class Program
    {
        static void Main()
        {
            User u = new User("ID#001", "John Snow", 42);
            u.PrintInfo();
            User.ChangeUserAge(u, 24);
            u.PrintInfo();
        }
    }
}