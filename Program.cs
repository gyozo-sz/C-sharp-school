using System.Runtime.CompilerServices;

namespace MyApp
{
    sealed class Worker
    {
        public int Rate;
        public int TotalHour;

        public Worker(int rate, int totalHour)
        {
            Rate = rate;
            TotalHour = totalHour;
        }

        public double CalculateSalary()
        {
            return Rate * TotalHour * 1.5;
        }
    }

    static class TopWorker
    {
        static public double CalculateSalaryWithBonus(this Worker worker)
        {
            if (worker.Rate < 50 && worker.TotalHour > 200)
            {
                return worker.Rate * worker.TotalHour * 2;
            } else 
            {
                return worker.CalculateSalary();
            } 
        }

        static public void CompareSalariesWithBonus(this Worker worker, string workerName)
        {
            Console.WriteLine($"{workerName}'s working info:");
            Console.WriteLine($"\tRate: {worker.Rate}");
            Console.WriteLine($"\tTotal Hours: {worker.TotalHour}");
            Console.WriteLine($"Normal Salary: {worker.CalculateSalary()}");
            Console.WriteLine($"Salary with Bonus: {worker.CalculateSalaryWithBonus()}");
            Console.WriteLine("\n");
        }
    }

    

    internal class Program
    {

        static void Main(string[] args)
        {
            Worker worker1 = new(100, 250);
            Worker worker2 = new(45, 250);

            worker1.CompareSalariesWithBonus("Worker 1");
            worker2.CompareSalariesWithBonus("Worker 2");
        }
    }
}