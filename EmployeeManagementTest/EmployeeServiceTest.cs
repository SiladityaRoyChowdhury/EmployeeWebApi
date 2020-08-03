using EmployeeManagement.Core;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EmployeeManagementTest
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> mockEmployeeRepository;
        public EmployeeServiceTest()
        {
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public async Task CheckGetEmployeesUsingMoqAsync()
        {
            mockEmployeeRepository.Setup(x => x.GetEmployees())
                                  .Returns(GetTestEmployees());
            var employeeService = new EmployeeService(mockEmployeeRepository.Object);
            var result = await employeeService.GetEmployees();
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckGetEmployeeUsingMoqAsync()
        {
            mockEmployeeRepository.Setup(x => x.GetEmployee(1))
                                  .Returns(GetTestEmployeeOne());
            var employeeService = new EmployeeService(mockEmployeeRepository.Object);
            var result = await employeeService.GetEmployee(1);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task CheckCreateEmployeeActionUsingMoqAsync()
        {
            mockEmployeeRepository.Setup(x => x.CreateEmployee(GetTestEmployeeThree()))
                                  .Returns(Task.FromResult(false));
            var employeeService = new EmployeeService(mockEmployeeRepository.Object);
            var result = await employeeService.CreateEmployee(GetTestEmployeeThree());
            Assert.False(result);
        }

        [Fact]
        public async Task CheckDeleteEmployeeActionUsingMoqAsync()
        {
            mockEmployeeRepository.Setup(x => x.GetEmployee(2))
                                 .Returns(GetTestEmployeeTwo());
            mockEmployeeRepository.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()))
                                  .Returns(Task.FromResult(true));
            var employeeService = new EmployeeService(mockEmployeeRepository.Object);
            var result = await employeeService.DeleteEmployee(GetTestEmployeeThree());
            Assert.True(result);
        }

        #region FakeData

        private Task<IEnumerable<Employee>> GetTestEmployees()
        {
            var data = new List<Employee>()
            {
                GetTestEmployeeOne().Result,
                GetTestEmployeeTwo().Result,
            };
            return Task.FromResult<IEnumerable<Employee>>(data);
        }

        private Task<Employee> GetTestEmployeeOne()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Alexander",
                LastName = "Hill",
                EmployeeAddress = "Sydney",
                Salary = 5000.00M,
                MobileNumber = "04140986545"
            };
            return Task.FromResult(employee);
        }

        private Task<Employee> GetTestEmployeeTwo()
        {
            var employee = new Employee
            {
                EmployeeId = 2,
                FirstName = "Roberts",
                LastName = "Dowry",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "04140986545"
            };
            return Task.FromResult(employee);
        }

        private Employee GetTestEmployeeThree()
        {
            return new Employee
            {
                EmployeeId = 2,
                FirstName = "Roberts",
                LastName = "Dowry",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "04140986545"
            };
        }

        #endregion
    }
}
