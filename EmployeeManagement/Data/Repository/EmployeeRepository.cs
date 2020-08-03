using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<bool> UpdateEmployee(Employee employee)
        {
            _context.Employees.AddOrUpdate(employee);
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Employee> GetEmployee(int employeeId)
        {
            return await _context.Employees.FindAsync(employeeId);
        }

        public async Task<bool> DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
            return (await _context.SaveChangesAsync() > 0);
        }        
    }
}