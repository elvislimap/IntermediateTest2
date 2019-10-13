using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Application.Services;
using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntermediateTest2.Application.Tests.Services
{
    public class EmployeeAppServiceTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private IEmployeeAppService _employeeAppService;

        public EmployeeAppServiceTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
        }


        [Fact]
        public void EmployeeAppService_AddOk()
        {
            var employee = GenerateEmployeeOk();
            var result = Add(employee);

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(employee.EmployeeId, result.Return.ToInt());
        }

        [Fact]
        public void EmployeeAppService_AddError()
        {
            var employee = new Employee
            {
                BirthDate = new DateTime(1992, 3, 11),
                MonthlySalary = 7000M
            };

            var result = Add(employee);

            Assert.NotNull(result);
            Assert.False(result.MessageErrors.Any());
            Assert.True(result.ValidationErrors.Any());
            Assert.Null(result.Return);
        }

        [Fact]
        public void EmployeeAppService_GetAll()
        {
            var employees = new List<Employee> { GenerateEmployeeOk() };

            _employeeRepository.Setup(e => e.GetAll()).Returns(employees);
            _employeeAppService = new EmployeeAppService(_employeeRepository.Object);

            var result = _employeeAppService.GetAll();

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(employees, (List<Employee>)result.Return);
        }


        private Result Add(Employee employee)
        {
            _employeeRepository.Setup(e => e.Insert(employee));
            _employeeAppService = new EmployeeAppService(_employeeRepository.Object);

            return _employeeAppService.Add(employee);
        }

        private Employee GenerateEmployeeOk()
        {
            return new Employee
            {
                Name = "Elvis Augusto de Lima",
                BirthDate = new DateTime(1992, 3, 11),
                MonthlySalary = 7000M
            };
        }
    }
}