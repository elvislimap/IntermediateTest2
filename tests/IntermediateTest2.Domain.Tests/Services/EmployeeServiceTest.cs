using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;
using IntermediateTest2.Domain.Services;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntermediateTest2.Domain.Tests.Services
{
    public class EmployeeServiceTest
    {
        private readonly Mock<IEmployeeRepository> _employeeRepository;
        private IEmployeeService _employeeService;

        public EmployeeServiceTest()
        {
            _employeeRepository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public void EmployeeService_CheckIfItBirthdayOk()
        {
            const int employeeId = 1;
            var employee = new Employee { EmployeeId = employeeId, BirthDate = DateTime.Now };

            Assert.True(CheckIfItBirthday(employee, out var messagesErrors));
            Assert.Null(messagesErrors);
        }

        [Fact]
        public void EmployeeService_CheckIfItBirthdayError()
        {
            const int employeeId = 1;
            var employee = new Employee { EmployeeId = employeeId, BirthDate = DateTime.Now.AddDays(-1) };

            Assert.False(CheckIfItBirthday(employee, out var messagesErrors));
            Assert.NotNull(messagesErrors);
        }

        [Fact]
        public void EmployeeService_GetValueBySalaryOk()
        {
            const int employeeId = 1;
            var employee = new Employee { EmployeeId = employeeId, MonthlySalary = 7000M, BirthDate = DateTime.Now };

            Assert.Equal(560M, GetValueBySalary(employee, out var messagesErrors));
            Assert.NotNull(messagesErrors);
            Assert.Empty(messagesErrors);
        }

        [Fact]
        public void EmployeeService_GetValueBySalaryError()
        {
            const int employeeId = 1;
            var employee = new Employee { EmployeeId = employeeId };

            Assert.NotEqual(560M, GetValueBySalary(employee, out var messagesErrors));
            Assert.NotNull(messagesErrors);
        }

        private bool CheckIfItBirthday(Employee employee, out List<string> messagesErrors)
        {
            _employeeRepository.Setup(e => e.Get(employee.EmployeeId)).Returns(employee);
            _employeeService = new EmployeeService(_employeeRepository.Object);

            return _employeeService.CheckIfItBirthday(employee.EmployeeId, out messagesErrors);
        }

        private decimal GetValueBySalary(Employee employee, out List<string> messagesErrors)
        {
            _employeeRepository.Setup(e => e.Get(employee.EmployeeId))
                .Returns(employee.MonthlySalary > 0 ? employee : null);
            _employeeService = new EmployeeService(_employeeRepository.Object);

            return _employeeService.GetValueBySalary(employee.EmployeeId, out messagesErrors);
        }
    }
}