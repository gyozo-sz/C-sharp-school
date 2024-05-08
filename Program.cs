using System;

namespace MyApp
{
    class Car
    {
        private int _quantity;
        private int _warranty;

        public int Quantity { get; private set; }
        public int Warranty { get; private set; }

        public Car() {
            Random r = new();
            Quantity = r.Next(10, 100);
            Warranty = r.Next(100, 1000);
        }

        virtual public void GetQuantity()
        {
            Console.WriteLine($"Cars ready for operation: {Quantity}");
        }
        virtual public void GetWarranty()
        {
            Console.WriteLine($"Warranty period for cars (days): {Warranty}");
        }

        virtual public void GetFullInfo()
        {
            GetQuantity();
            GetWarranty();
            Console.WriteLine('\n');
        }

        public void SetWarranty(int newWarranty)
        {
            _warranty = newWarranty;
        }
    }

    class VolksWagen : Car {
        public VolksWagen() : base() { 
        }

        public override void GetQuantity()
        {
            Console.WriteLine($"VolksWagen ready for operation: {Quantity}");
        }

        sealed public override void GetFullInfo()
        {
            Console.WriteLine("VolksWagen full info:");
            base.GetFullInfo();
            
        }
    }

    class Toyota : Car {
        public override void GetQuantity()
        {
            Console.WriteLine($"Toyota ready for operation: {Quantity}");
        }

        sealed public override void GetFullInfo()
        {
            Console.WriteLine("Toyota full info:");
            base.GetFullInfo();
        }
    }

    class Audi : VolksWagen
    {
        public override void GetQuantity()
        {
            Console.WriteLine($"Audi ready for operation: {Quantity}");
        }
    }

    internal class Program
    {
        static void Main()
        {
            Toyota t = new();
            t.GetFullInfo();

            VolksWagen w = new();
            w.GetFullInfo();

            Audi a = new();
            a.GetFullInfo();
        }
    }
}