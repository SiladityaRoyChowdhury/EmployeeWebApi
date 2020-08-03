using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int employeeId);

        Task<bool> CreateEmployee(Employee employee);

        Task<bool> DeleteEmployee(Employee employee);

        Task<bool> UpdateEmployee(Employee employee);
    }
}