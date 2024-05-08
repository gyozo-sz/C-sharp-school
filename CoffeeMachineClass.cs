using CoffeeMakingSteps;
using CoffeeRecipes;
using CoffeeTypes;
using MachineClass;

namespace CoffeeMachineClass
{
    class CoffeeMachine : Machine
    {
        protected List<CoffeeRecipe<Coffee>> _recipes;

        public CoffeeMachine()
        {
            _recipes = new List<CoffeeRecipe<Coffee>>();
        }

        public override bool SelfClean()
        {
            Console.WriteLine("Cleaning tray...");
            Console.WriteLine("Cleaning containers...");
            Console.WriteLine("Cleaning pipes...");
            Console.WriteLine("Cleaning completed");
            return true;
        }

        public Coffee MakeCoffee(string recipeName)
        {
            if (!Powered)
            {
                throw new Exception("CoffeeMachine cannot be used without power.");

            }
            foreach (CoffeeRecipe<Coffee> recipe in _recipes)
            {
                if (recipe.Name == recipeName)
                {
                    foreach (CoffeeMakingStep coffeeMakingStep in recipe.Steps)
                    {
                        coffeeMakingStep.Execute();
                        AddWear(coffeeMakingStep.AddedWear());
                    }
                    return recipe.MakeCoffee();
                }

            }
            throw new Exception("No such coffee exists among the machine's recipes.");
        }
    }

    class PhilipsMachine : CoffeeMachine
    {
        public PhilipsMachine()
        {
            _recipes.Add(new FlatWhiteRecipe());
            _recipes.Add(new CappuccinoRecipe());
        }
    }
}
