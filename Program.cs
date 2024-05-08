using System;

namespace MyApp
{
    abstract class Animal
    {
        abstract public void Sound();
    }

    class Cat : Animal, IAnimal, ISleepable, IRunable, IEatable
    {
        public override void Sound()
        {
            Console.WriteLine("Meow");
        }

        public void Run()
        {
            Console.WriteLine("It do be runnin'.");
        }

        public void Eat()
        {
            Console.WriteLine("Feeding time!");
        }
    }

    class Dog : Animal, IAnimal, ISleepable, IRunable, IEatable
    {
        override public void Sound()
        {
            Console.WriteLine("Woof");
        }

        public void Run()
        {
            Console.WriteLine("Fly, you fools");
        }

        public void Eat()
        {
            Console.WriteLine("Chomp");
        }
    }

    public interface IAnimal { }

    public interface IRunable {
        void Run();
    }

    public interface IEatable {
        void Eat();
    }

    interface ISleepable {
        void Sleep() { Console.WriteLine("Zzzzz..."); }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dog method calls:");
            Dog doggy = new();
            doggy.Sound();
            doggy.Eat();
            doggy.Run();

            ISleepable sleepableDoggy = doggy;
            sleepableDoggy.Sleep();

            Console.WriteLine("\nCat method calls:");
            Cat cat = new();
            cat.Sound();
            cat.Eat();
            cat.Run();

            ISleepable sleepableCat = cat;
            sleepableCat.Sleep();


        }
    }
}