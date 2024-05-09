namespace CoffeeTypes
{
    public class Coffee
    {
        protected string _name;

        public Coffee(string name) => _name = name;
        public Coffee() => _name = "";

        public string Name { get => _name; set => _name = value; }

        public void Drink()
        {
            Console.WriteLine($"Drinking coffee: { GetType().Name }");
        }
    }

    public class FlatWhite : Coffee
    {
        public FlatWhite() : base("Flat White") { }
    }

    public class Cappuccino : Coffee
    {
        public Cappuccino() : base("Cappuccino") { }
    }

    public class Americano : Coffee
    {
        public Americano() : base("Americano") { }
    }
}
