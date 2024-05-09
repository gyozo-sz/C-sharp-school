using System;
using System.Numerics;

namespace MyApp
{

    class Employee
    {
        private string _name;
        private uint _salary;
        private uint _bonus;

        public string Name { 
            get
            {
                string[] split_names = _name.Split(' ');
                return split_names[split_names.Length - 1];
            }
        }
        public uint Salary
        {
            get => _salary;
            set => _salary = value;
        }

        public Employee (string name, uint salary)
        {
            _name = name;
            _salary = salary;
        }

        virtual public void SetBonus(uint bonus)
        {
            _bonus = bonus;
        }

        public uint ToPay()
        {
            return Salary + _bonus;
        }
    }

    class SalesPerson : Employee
    {
        private uint _percent;

        public SalesPerson(string name, uint salary, uint percent) : base(name, salary) => _percent = percent;

        public override void SetBonus(uint bonus)
        {
            base.SetBonus(_percent > 200 ? bonus * 3 : _percent > 100 ? bonus * 2 : bonus);
        }
    }

    class Manager : Employee
    {
        private uint _quantity;

        public Manager(string name, uint salary, uint clientAmount) : base(name, salary) => _quantity = clientAmount;

        public override void SetBonus(uint bonus)
        {
            base.SetBonus(_quantity > 150 ? bonus + 1000 : _quantity > 100 ? bonus + 500 : bonus);
        }
    }

    class Company
    {
        private Employee[] _employees;

        public Company(Employee[] employees) => _employees = employees;

        public Employee[] Employees { get => _employees; }

        public void GiveEverybodyBonus(uint companyBonus)
        {
            foreach (Employee employee in _employees)
            {
                employee.SetBonus(companyBonus);
            }
        }

        public uint TotalToPay() {
            return Convert.ToUInt32(_employees.Sum(x => Convert.ToInt64(x.ToPay())));
        }

        // Not exactly clear what the task expects from me but this is my implementation:
        // "Create the GetNameSalary() method that returns the employee's last name and the amount of his/her compensation including bonus."
        public (string, uint) GetNameSalary(uint employeeIdx)
        {
            return (_employees[employeeIdx].Name, _employees[employeeIdx].Salary);
        }
    }

    static class EmployeePrintExtension
    {
        static public void PrintEmployeeInfo(Employee employee)
        {
            Console.WriteLine($"Employee name: {employee.Name}");
            Console.WriteLine("Employee type: {0}", employee is Manager ? "Manager" : "Sales Person");
            Console.WriteLine($"Employee salary: {employee.Salary}");
            Console.WriteLine($"Employee total pay: {employee.ToPay()}");
        }
    }

    static class CompanyPrintExtension
    {
        static public void PrintAllEmployeeInfo(Company company)
        {
            foreach (Employee employee in company.Employees)
            {
                EmployeePrintExtension.PrintEmployeeInfo(employee);
                Console.WriteLine("\n");
            }
        }
    }

    internal class Program
    {
        

        static void Main()
        {
            SalesPerson johnny = new("Jon Snow", 1000, 163);
            SalesPerson jaime = new("Jaime Lannister", 800, 33);
            SalesPerson sansa = new("Sansa Stark", 750, 333);

            Manager rey = new("Rey Skywalker", 10000, 164);
            Manager han = new("Han Solo", 8000, 123);
            Manager chewy = new("Chewbacca", 15000, 35);

            Company company = new(new Employee[] { johnny, jaime, sansa, rey, han, chewy });
            uint bonusAmount = 200;
            Console.WriteLine($"Bonus amount is: {bonusAmount}\n");
            company.GiveEverybodyBonus(bonusAmount);

            CompanyPrintExtension.PrintAllEmployeeInfo(company);

            Console.WriteLine($"Total amount to pay: {company.TotalToPay()}");

            

        }
    }
}