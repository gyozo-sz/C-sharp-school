namespace CoffeeTypes
{
    class Coffee
    {
        protected string _name;

        public Coffee(string name) => _name = name;
        public Coffee() => _name = "";

        public string Name { get => _name; set => _name = value; }
    }

    class FlatWhite : Coffee
    {
        public FlatWhite() : base("Flat White") { }
    }

    class Cappuccino : Coffee
    {
        public Cappuccino() : base("Cappuccino") { }
    }
}
