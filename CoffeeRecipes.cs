using CoffeeMakingSteps;
using CoffeeTypes;

namespace CoffeeRecipes
{

    class CoffeeRecipeList
    {
        public List<ICoffeeRecipe> recipeList;
    }

    abstract class ICoffeeRecipe
    {

    }

    class CoffeeRecipe<CoffeeType> : ICoffeeRecipe where CoffeeType : Coffee, new()
    {
        protected string _name;
        protected CoffeeMakingStep[] _steps;

        public string Name { get => _name; }
        public CoffeeMakingStep[] Steps { get => _steps; }

        public CoffeeRecipe(string name, CoffeeMakingStep[] steps) {
            _name = name;
            _steps = steps;
        }

        public CoffeeType MakeCoffee()
        {
            foreach (CoffeeMakingStep step in _steps)
            {
                step.Execute();
            }
            return new CoffeeType();
        }
    }

    class FlatWhiteRecipe : CoffeeRecipe<FlatWhite>
    {
        static readonly CoffeeMakingStep[] flatWhiteSteps = { new MakeExpressoStep(), new MakeSteamedMilkStep() };

        public FlatWhiteRecipe() : base("Flat White", flatWhiteSteps) { }
    }

    class CappuccinoRecipe : CoffeeRecipe<Cappuccino> 
    {
        static readonly CoffeeMakingStep[] cappuccinoSteps = { new MakeExpressoStep(), new MakeSteamedMilkStep(), new MakeMilkFoamStep() };

        public CappuccinoRecipe() : base("Cappuccino", cappuccinoSteps) { }
    }
}
