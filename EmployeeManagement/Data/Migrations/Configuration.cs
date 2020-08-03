namespace EmployeeManagement.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmployeeManagement.Data.EmployeeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EmployeeManagement.Data.EmployeeContext context)
        {
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(EmployeeData());
                context.SaveChanges();
            }
        }
        
        private static List<Employee> EmployeeData()
        {
            return new List<Employee>()
            {
                new Employee() { EmployeeId = 1, FirstName = "Raj", LastName ="Kumar", EmployeeAddress = "Bangalore", MobileNumber = "09876512345", Salary = 23443.40M },
                new Employee() { EmployeeId = 2, FirstName = "Rahul", LastName ="Patel", EmployeeAddress = "Gujrat", MobileNumber = "09876513345", Salary = 53443.40M },
                new Employee() { EmployeeId = 3, FirstName = "Shekhar", LastName ="Chowdhury", EmployeeAddress = "Delhi", MobileNumber = "09276512345", Salary = 33443.40M },
                new Employee() { EmployeeId = 4, FirstName = "Abhishek", LastName ="Roy", EmployeeAddress = "Kolkata", MobileNumber = "09876512312", Salary = 43443.40M },
                new Employee() { EmployeeId = 5, FirstName = "Rita", LastName ="Kumari", EmployeeAddress = "Chennai", MobileNumber = "09376512345", Salary = 24443.40M }
            };
        }
    }
}
