using CoffeeTypes;

namespace CoffeeRecipes
{
    public enum CoffeeMakingStep
    {
        MakeExpresso = 0,
        MakeFoamedMilk = 1,
        MakeSteamedMilk = 2,
        MakeHotWater = 3
    }

    public class CoffeeRecipe<CoffeeType>
        where CoffeeType : Coffee, new()
    {
        private CoffeeMakingStep[] _steps;
        private uint _completedSteps;

        public CoffeeMakingStep[] Steps { get => _steps; }

        public CoffeeRecipe(CoffeeMakingStep[] steps)
        {
            _steps = steps;
            _completedSteps = 0;
        }

        public CoffeeType CreateCoffee()
        {
            return new CoffeeType();
        }
    }

    public static class CoffeeRecipeBook
    {
        public static CoffeeRecipe<Cappuccino> CappuccinoRecipe()
        {
            return new CoffeeRecipe<Cappuccino>(new CoffeeMakingStep[] { CoffeeMakingStep.MakeExpresso, CoffeeMakingStep.MakeSteamedMilk, CoffeeMakingStep.MakeFoamedMilk });
        }

        public static CoffeeRecipe<Americano> AmericanoRecipe()
        {
            return new CoffeeRecipe<Americano>(new CoffeeMakingStep[] { CoffeeMakingStep.MakeExpresso, CoffeeMakingStep.MakeHotWater });
        }

        public static CoffeeRecipe<FlatWhite> FlatWhiteRecipe()
        {
            return new CoffeeRecipe<FlatWhite>(new CoffeeMakingStep[] { CoffeeMakingStep.MakeExpresso, CoffeeMakingStep.MakeSteamedMilk });
        }
    }
}
