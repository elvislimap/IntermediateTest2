using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations;
using System;
using Xunit;

namespace IntermediateTest2.Domain.Tests.Entities
{
    public class EmployeeTest
    {
        [Fact]
        public void Employee_Valid()
        {
            var employee = new Employee
            {
                Name = "Elvis Augusto de Lima",
                BirthDate = new DateTime(1992, 3, 11),
                MonthlySalary = 7000M
            };

            Assert.True(employee.IsValid(out var validationErrors));
            Assert.NotNull(validationErrors);
        }

        [Fact]
        public void Employee_InvalidRequired()
        {
            var employee = new Employee();

            Assert.False(employee.IsValid(out var validationErrors));
            Assert.NotNull(validationErrors);
            Assert.Equal(3, validationErrors.Count);
        }

        [Fact]
        public void Employee_InvalidName()
        {
            var employee = new Employee
            {
                Name = "Nam quis nulla. Integer malesuada. In in enim a arcu imperdiet malesuada. Sed vel lectus. Donec odio u",
                BirthDate = new DateTime(1992, 3, 11),
                MonthlySalary = 7000M
            };

            Assert.False(employee.IsValid(out var validationErrors));
            Assert.NotNull(validationErrors);
        }
    }
}