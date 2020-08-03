using EmployeeManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core
{
    public interface IEmployeeService
    {
        Task<bool> CreateEmployee(Employee employee);

        Task<IEnumerable<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int employeeId);

        Task<bool> DeleteEmployee(Employee employee);
    }
}