using CoffeeMachineClass;
using BarristaClass;
using CoffeeTypes;
using CoffeeRecipes;

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
                peter.MakeCoffeeWithMachine(philipsMachine, CoffeeRecipeBook.CappuccinoRecipe());
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            philipsMachine.TurnOn();
            Cappuccino c = peter.MakeCoffeeWithMachine(philipsMachine, CoffeeRecipeBook.CappuccinoRecipe());
            c.Drink();

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);

            FlatWhite fw = caitlyn.MakeCoffeeWithMachine(philipsMachine, CoffeeRecipeBook.FlatWhiteRecipe());
            fw.Drink();

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);

            
            peter.PrintExperiences();
            caitlyn.PrintExperiences();
            
            Console.WriteLine('\n');

            philipsMachine.Clean();

            Console.WriteLine("\nPhilips Coffee Machine current wear: {0}", philipsMachine.Wear);

            BasicCoffeeMachine basicMachine = new();
            basicMachine.TurnOn();
            try
            {
                FlatWhite fw2 = caitlyn.MakeCoffeeWithMachine(basicMachine, CoffeeRecipeBook.FlatWhiteRecipe());
            } 
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }
    }
}