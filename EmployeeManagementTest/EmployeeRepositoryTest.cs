using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public void DeleteEmployeeViacContextPassTest()
        {
            var mockset = new Mock<DbSet<Employee>>();
            var mockcontext = new Mock<EmployeeContext>();
            mockset.Setup(s => s.Create())
                .Returns(() =>
                {
                    Mock<Employee> mock = new Mock<Employee>();
                    return mock.Object;
                });
            mockcontext.Setup(m => m.Set<Employee>())
                .Returns(() => mockset.Object);
            var employeeRepository = new EmployeeRepository(mockcontext.Object);
            var result = employeeRepository.DeleteEmployee(GetTestEmployeeThree());
            Assert.False(result.Result);
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
                EmployeeId = 1,
                FirstName = "Dowry",
                LastName = "Dowri",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "0414098650"
            };
        }
    }
}
