using System;
using System.Data.Common;

namespace MyApp
{
    

    class Coffee
    {
        protected string _name;

        public string Name { get { return _name; } }
    }

    class FlatWhite : Coffee
    {
        public FlatWhite()
        {
            _name = "Flat White";
        }
    }

    class Cappuccino : Coffee
    {
        public Cappuccino()
        {
            _name = "Cappuccino";
        }
    }

    abstract class Machine
    {
        private bool _powered = false;

        public bool Powered
        {
            get { return _powered; }
        }

        private uint _wear = 0;

        public uint Wear { get { return _wear; } }

        public void TurnOn()
        {
            _powered = true;
        }

        public void TurnOff()
        {
            _powered = false;
        }

        public void AddWear(uint wear)
        {
            _wear += wear;
        }

        public bool Clean()
        {
            if (!_powered)
            {
                Console.WriteLine("Machine has to be turned on for cleaning!");
                return false;
            }

            bool cleanSuccess = SelfClean();
            if (cleanSuccess)
            {
                _wear = 0;
            }

            return cleanSuccess;
        }

        abstract public bool SelfClean();
    }

    abstract class CoffeeMakingStep
    {
        abstract public void Execute();
        abstract public uint AddedWear();
    }

    class MakeExpressoStep : CoffeeMakingStep
    {
        override public void Execute()
        {
            Console.WriteLine("Making expresso...");
        }

        public override uint AddedWear()
        {
            return 5;
        }
    }

    class MakeMilkFoamStep : CoffeeMakingStep
    {
        override public void Execute()
        {
            Console.WriteLine("Making milk foam...");
        }

        public override uint AddedWear()
        {
            return 2;
        }
    }

    class MakeSteamedMilkStep : CoffeeMakingStep
    {
        override public void Execute()
        {
            Console.WriteLine("Making steamed milk...");
        }

        public override uint AddedWear()
        {
            return 3;
        }
    }

    abstract class CoffeeRecipe
    {
        protected string _name;
        
        public string Name {
            get { return _name; } 
        }
        protected CoffeeMakingStep[] _steps;

        public CoffeeMakingStep[] Steps { get { return _steps; } }

        abstract public Coffee MakeCoffee();
    }

    class FlatWhiteRecipe : CoffeeRecipe
    {
        public FlatWhiteRecipe()
        {
            _name = "Flat White";
            _steps = new CoffeeMakingStep[2] { new MakeExpressoStep(), new MakeSteamedMilkStep() };
        }

        override public Coffee MakeCoffee()
        {
            return new FlatWhite();
        }
    }

    class CappuccinoRecipe : CoffeeRecipe
    {
        public CappuccinoRecipe()
        {
            _name = "Cappuccino";
            _steps = new CoffeeMakingStep[3] { new MakeExpressoStep(), new MakeSteamedMilkStep(), new MakeMilkFoamStep() };
        }

        override public Coffee MakeCoffee()
        {
            return new Cappuccino();
        }
    }

    class CoffeeMachine : Machine
    {
        protected List<CoffeeRecipe> _recipes;

        public CoffeeMachine()
        {
            _recipes = new List<CoffeeRecipe>();
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
            foreach (CoffeeRecipe recipe in _recipes)
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

    class RecipeExperience
    {
        private string _recipeName;
        private uint _timesPrepared;

        public string RecipeName {
            get { return _recipeName; }
        }

        public uint TimesPrepared { get { return _timesPrepared; } }
        
        public RecipeExperience(string name)
        {
            _recipeName = name;
            _timesPrepared = 1;
        }

        public void AddExperience()
        {
            _timesPrepared++;
        }
    }

    class Barrista
    {
        public readonly string _name;
        public List<RecipeExperience> _experiences;

        public Barrista(string name)
        {
            _name = name;
            _experiences = new List<RecipeExperience>();
        }

        public void PrintExperiences()
        {
            Console.WriteLine($"\n{_name}'s current experiences:");
            foreach (RecipeExperience experience in _experiences)
            {
                Console.WriteLine("\t{0}: {1}", experience.RecipeName, experience.TimesPrepared);
            }
        }

        private void AddExperience(string recipeName)
        {
            foreach (RecipeExperience recipeExperience in _experiences)
            {
                if (recipeExperience.RecipeName == recipeName)
                {
                    recipeExperience.AddExperience();
                    return;
                }
            }
            _experiences.Add(new RecipeExperience(recipeName));
        }

        public Coffee MakeCoffeeWithMachine(CoffeeMachine coffeeMachine, string recipeName)
        {
            Coffee coffee = coffeeMachine.MakeCoffee(recipeName);
            AddExperience(recipeName);

            return coffee;
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