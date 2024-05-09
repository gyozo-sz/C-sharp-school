using CoffeeRecipes;
using CoffeeTypes;
using MachineClass;

namespace CoffeeMachineClass
{
    

    class CoffeeMachineOperation : MachineOperation<CoffeeMakingStep>
    {
        public CoffeeMachineOperation(CoffeeMakingStep operation, uint addedWear) : base(operation, addedWear) { }
    }

    class CoffeeMachine : Machine<CoffeeMakingStep>
    {
        protected List<CoffeeMachineOperation> _operations;

        public CoffeeMachine()
        {
            _operations = new List<CoffeeMachineOperation> { 
                new CoffeeMachineOperation(CoffeeMakingStep.MakeExpresso, 5),
                new CoffeeMachineOperation(CoffeeMakingStep.MakeHotWater, 3) 
            };
        }

        public override bool SelfClean()
        {
            Console.WriteLine("Cleaning tray...");
            Console.WriteLine("Cleaning containers...");
            Console.WriteLine("Cleaning pipes...");
            Console.WriteLine("Cleaning completed");
            return true;
        }

        public CoffeeType MakeCoffee<CoffeeType>(CoffeeRecipe<CoffeeType> recipe)
            where CoffeeType : Coffee, new()
        {
            if (!Powered)
            {
                throw new Exception("CoffeeMachine cannot be used without power.");

            }
            foreach (CoffeeMakingStep step in recipe.Steps)
            {
                if (_operations.FirstOrDefault(op => op.Operation == step) is CoffeeMachineOperation operation)
                {
                    PerformOperation(operation);
                }else
                {
                    throw new Exception($"Unknown operation for CoffeeMachine: {step}");
                }
            }
            return recipe.CreateCoffee();
            
        }
    }

    class PhilipsMachine : CoffeeMachine
    {
        public PhilipsMachine()
        {
            _operations.Add(new CoffeeMachineOperation(CoffeeMakingStep.MakeFoamedMilk, 10) );
            _operations.Add(new CoffeeMachineOperation(CoffeeMakingStep.MakeSteamedMilk, 8) );
        }
    }
    class BasicCoffeeMachine : CoffeeMachine
    {
        
    }
}
