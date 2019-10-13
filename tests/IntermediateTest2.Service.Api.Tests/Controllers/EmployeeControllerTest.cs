using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Security.Interfaces;
using IntermediateTest2.Service.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntermediateTest2.Service.Api.Tests.Controllers
{
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployeeAppService> _employeeAppService;
        private EmployeeController _employeeController;

        public EmployeeControllerTest()
        {
            _employeeAppService = new Mock<IEmployeeAppService>();
        }


        [Fact]
        public async Task EmployeeController_Add()
        {
            var result = new Result(1);
            var employee = new Employee { Name = "Abc", BirthDate = DateTime.Now, MonthlySalary = 5000M };

            _employeeAppService.Setup(e => e.Add(employee)).Returns(result);
            _employeeController = new EmployeeController(_employeeAppService.Object);

            var objectResult = await _employeeController.Add(employee);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }

        [Fact]
        public async Task EmployeeController_GetAll()
        {
            var employee = new Employee { Name = "Abc", BirthDate = DateTime.Now, MonthlySalary = 5000M };
            var result = new Result(employee);

            _employeeAppService.Setup(e => e.GetAll()).Returns(result);
            _employeeController = new EmployeeController(_employeeAppService.Object);

            var objectResult = await _employeeController.GetAll();

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }
    }
}