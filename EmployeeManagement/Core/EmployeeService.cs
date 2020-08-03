using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeManagement.Core
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Task<bool> CreateEmployee(Employee employee)
        {
            return _employeeRepository.CreateEmployee(employee);
        }

        public Task<bool> DeleteEmployee(Employee employee)
        {
            return _employeeRepository.DeleteEmployee(employee);
        }

        public Task<Employee> GetEmployee(int employeeId)
        {
            return _employeeRepository.GetEmployee(employeeId);
        }

        public Task<IEnumerable<Employee>> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }
    }
}