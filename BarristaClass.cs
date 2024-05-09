using CoffeeMachineClass;
using CoffeeRecipes;
using CoffeeTypes;

namespace BarristaClass
{
    class RecipeExperience
    {
        private string _recipeName;
        private uint _timesPrepared;

        public string RecipeName
        {
            get => _recipeName;
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

        public CoffeeType MakeCoffeeWithMachine<CoffeeType>(CoffeeMachine coffeeMachine, CoffeeRecipe<CoffeeType> coffeeRecipe)
            where CoffeeType : Coffee, new()
        {
            CoffeeType coffee = coffeeMachine.MakeCoffee(coffeeRecipe);
            AddExperience(typeof(CoffeeType).Name);

            return coffee;
        }
    }

    
}
