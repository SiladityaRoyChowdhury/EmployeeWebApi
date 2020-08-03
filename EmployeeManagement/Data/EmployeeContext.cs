using EmployeeManagement.Data.Migrations;
using System;
using System.Data.Entity;

namespace EmployeeManagement.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("EmployeeConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EmployeeContext, Configuration>());
        }

        public DbSet<Employee> Employees { get; set; }
    }
}