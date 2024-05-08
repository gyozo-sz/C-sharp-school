using CoffeeMachineClass;
using BarristaClass;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Barrista peter = new Barrista("Peter");
            Barrista caitlyn = new Barrista("Caitlyn");

            PhilipsMachine philipsMachine = new PhilipsMachine();

            try
            {
                peter.MakeCoffeeWithMachine(philipsMachine, "Cappuccino");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            philipsMachine.TurnOn();
            peter.MakeCoffeeWithMachine(philipsMachine, "Cappuccino");

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);

            caitlyn.MakeCoffeeWithMachine(philipsMachine, "Flat White");

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);

            
            peter.PrintExperiences();
            caitlyn.PrintExperiences();
            
            Console.WriteLine('\n');

            philipsMachine.Clean();

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);


        }
    }
}