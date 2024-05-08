using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMakingSteps
{
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
}
