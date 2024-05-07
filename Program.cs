using System;
using System.Reflection.Metadata.Ecma335;

namespace MyApp
{
    class Coffee
    {
        private  string _name;
        private  int _price;
        public string Name { 
            get => _name;
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _name = value;
                }
            }
        }
        public int Price
        {
            get => _price;
            set
            {
                if (value > 0)
                {
                    _price = value;
                }
            }
        }

        public Coffee(string name, int price)
        {
            Name = name;
            Price = price;
        }

        private void GetInfo()
        {
            Console.WriteLine("Coffee info:");
            Console.WriteLine($"\tName: {Name}");
            Console.WriteLine($"\tPrice: {Price}");
        }

        public void PrintInfo()
        {
            GetInfo();
            Console.WriteLine("-----------------------------");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Coffee cappuccino = new("Cappuccino", 50);
            cappuccino.PrintInfo();
        }
    }
}