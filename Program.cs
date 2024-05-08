using System;

namespace MyApp
{
    class User
    {
        private int _userId;
        private int _balance;

        public int UserId { get => _userId; set => _userId = value; }
        public int Balance { get => _balance; set => _balance = value; }
    }

    class Provider<U> where U: User
    {
        public void CheckBalance(U user)
        {
            Console.WriteLine($"Balance of the user is: {user.Balance}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            User u = new();
            u.UserId = 1234;
            u.Balance = 1000;

            Provider<User> p = new();
            p.CheckBalance(u);
        }
    }
}