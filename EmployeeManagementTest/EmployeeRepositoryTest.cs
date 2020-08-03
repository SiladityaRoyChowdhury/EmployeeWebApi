using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using Xunit;

namespace EmployeeManagementTest
{
    public class EmployeeRepositoryTest
    {
        [Fact]
        public void CreateEmployeeViacContextTest()
        {
            var mockEmployeeContext = new EmployeeContext();
            var service = new EmployeeRepository(mockEmployeeContext);
            var result = service.CreateEmployee(GetTestEmployeeThree());
            Assert.True(result.Result);
        }

        [Fact]
        public void DeleteEmployeeViacContextFailTest()
        {
            var mockEmployeeContext = new EmployeeContext();
            var service = new EmployeeRepository(mockEmployeeContext);
            var result = service.DeleteEmployee(GetTestEmployeeTwo());
            Assert.False(result.Result);
        }

        [Fact]
        public void DeleteEmployeeViacContextPassTest()
        {
            var mockEmployeeContext = new EmployeeContext();
            var service = new EmployeeRepository(mockEmployeeContext);
            if (service.CreateEmployee(GetTestEmployeeThree()).Result)
            {
                var result = service.DeleteEmployee(GetTestEmployeeThree());
                Assert.True(result.Result);
            }
        }

        [Fact]
        public void GetEmployeeViacContextTest()
        {
            var mockEmployeeContext = new EmployeeContext();
            var service = new EmployeeRepository(mockEmployeeContext);
            var result = service.GetEmployee(1);
            Assert.NotNull(result.Result);
            Assert.IsType<Employee>(result.Result);
        }

        [Fact]
        public void GetEmployeesViacContextTest()
        {
            var mockEmployeeContext = new EmployeeContext();
            var service = new EmployeeRepository(mockEmployeeContext);
            var result = service.GetEmployees();
            Assert.NotNull(result.Result);
        }

        private Employee GetTestEmployeeTwo()
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

        private Employee GetTestEmployeeThree()
        {
            return new Employee
            {
                EmployeeId = 8,
                FirstName = "Dowry",
                LastName = "Dowri",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "0414098650"
            };
        }
    }
}
