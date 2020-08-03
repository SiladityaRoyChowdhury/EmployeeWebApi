using AutoMapper;
using EmployeeManagement.Controllers;
using EmployeeManagement.Core;
using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Xunit;

namespace EmployeeManagementTest
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeService> mockEmployeeService;
        private readonly IMapper mapper;

        public EmployeeControllerTest()
        {
            mockEmployeeService = new Mock<IEmployeeService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeMappingProfile());
            }); 
            mapper = config.CreateMapper(); 
        }

        #region Moq

        [Fact]
        public async Task CheckGetActionUsingMoqAsync()
        {
            mockEmployeeService.Setup(x => x.GetEmployees())
                                  .Returns(GetTestEmployees());
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);

            // Act
            var result = await employeeController.Get();
            Assert.IsType<OkNegotiatedContentResult<IEnumerable<EmployeeModel>>>(result);
            var contentResult = result as OkNegotiatedContentResult<IEnumerable<EmployeeModel>>;
            var product = contentResult.Content;
            Assert.NotNull(product);
        }

        [Fact]
        public void CheckGetActionTest()
        {
            mockEmployeeService.Setup(x => x.GetEmployees())
                                 .Returns(GetTestEmployees());

            var employeeController = new EmployeeController(null, mapper);
            var result = employeeController.Get().Result;
            Assert.IsType<ExceptionResult>(result);
        }

        [Fact]
        public async Task CheckGetEmployeeActionUsingMoqAsync()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(1))
                                  .Returns(GetTestEmployeeOne());
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);

            // Act
            var result = await employeeController.Get(1);
            Assert.IsType<OkNegotiatedContentResult<EmployeeModel>>(result);
            var contentResult = result as OkNegotiatedContentResult<EmployeeModel>;
            var product = contentResult.Content;
            Assert.NotNull(product);
        }

        [Fact]
        public void CheckGetEmployeeActionTest()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(1))
                                 .Returns(GetTestEmployeeOne);

            var employeeController = new EmployeeController(null, mapper);
            var result = employeeController.Get(1).Result;
            Assert.IsType<ExceptionResult>(result);
        }

        [Fact]
        public void CheckGetEmployeeActionBadRequestTest()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(1))
                                 .Returns(GetTestEmployeeOne);

            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = employeeController.Get(0).Result;
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact]
        public void CheckGetEmployeeActionNotFoundResultTest()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(1))
                                  .Returns(GetTestEmployeeFour);

            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = employeeController.Get(100).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CheckPostEmployeeActionUsingMoqAsync()
        {
            mockEmployeeService.Setup(x => x.CreateEmployee(GetTestEmployeeThree()))
                                  .Returns(Task.FromResult(false));
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = await employeeController.PostAsync(GetTestEmployeeModelOne());
            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public async Task CheckPostEmployeeActionInternalServerErrorTest()
        {
            var employeeController = new EmployeeController(null, mapper);
            var result = await employeeController.PostAsync(GetTestEmployeeModelOne());
            Assert.IsType<ExceptionResult>(result);
        }

        [Fact]
        public async Task CheckPostEmployeeActionBadRequestTest()
        {
            mockEmployeeService.Setup(x => x.CreateEmployee(It.IsAny<Employee>()))
                                  .Returns(Task.FromResult(true));
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = await employeeController.PostAsync(GetTestEmployeeModelTwo());
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact]
        public async Task CheckDeleteEmployeeActionUsingMoqAsync()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(2))
                                 .Returns(GetTestEmployeeTwo());
            mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()))
                                  .Returns(Task.FromResult(true));
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = await employeeController.DeleteAsync(2);
            Assert.IsType<StatusCodeResult>(result);
            var contentResult = result as StatusCodeResult;
            Assert.Equal("NoContent", contentResult.StatusCode.ToString());
        }

        [Fact]
        public async Task CheckDeleteEmployeeInternalServerErrorTest()
        {
            mockEmployeeService.Setup(x => x.GetEmployee(2))
                                 .Returns(GetTestEmployeeTwo());
            mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()))
                                  .Returns(Task.FromResult(false));
            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = await employeeController.DeleteAsync(2);
            Assert.IsType<InternalServerErrorResult>(result);
        }

        [Fact]
        public void CheckDeleteEmployeeActionTest()
        {
            var employeeController = new EmployeeController(null, mapper);
            var result = employeeController.DeleteAsync(1).Result;
            Assert.IsType<ExceptionResult>(result);
        }

        [Fact]
        public void CheckDeleteEmployeeActionBadRequestTest()
        {
            mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()))
                                 .Returns(Task.FromResult(true));

            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = employeeController.DeleteAsync(0).Result;
            Assert.IsType<InvalidModelStateResult>(result);
        }

        [Fact]
        public void CheckDeleteEmployeeActionNotFoundResultTest()
        {
            mockEmployeeService.Setup(x => x.DeleteEmployee(It.IsAny<Employee>()))
                                  .Returns(Task.FromResult(true));
            mockEmployeeService.Setup(x => x.GetEmployee(100))
                                 .Returns(GetTestEmployeeFour);

            var employeeController = new EmployeeController(mockEmployeeService.Object, mapper);
            var result = employeeController.DeleteAsync(100).Result;
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion


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
                EmployeeId = 3,
                FirstName = "Dowry",
                LastName = "Dowry",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "0414098654598098"
            };
        }

        private Task<Employee> GetTestEmployeeFour()
        {
            Employee employee = null;
            return Task.FromResult(employee);
        }

        private EmployeeModel GetTestEmployeeModelOne()
        {
            return new EmployeeModel
            {
                EmployeeId = 3,
                FirstName = "Robert",
                LastName = "Dowry",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "0414098654598098"
            };
        }

        private EmployeeModel GetTestEmployeeModelTwo()
        {
            return new EmployeeModel
            {
                EmployeeId = 3,
                FirstName = "Dowry",
                LastName = "Dowry",
                EmployeeAddress = "GoldCoast",
                Salary = 5000.00M,
                MobileNumber = "0414098654598098797979709"
            };
        }
        #endregion
    }
}
