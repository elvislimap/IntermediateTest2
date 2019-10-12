using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations;
using System;
using System.Linq;
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

            var validationErrors = employee.IsValid();

            Assert.NotNull(validationErrors);
            Assert.False(validationErrors.Any());
        }

        [Fact]
        public void Employee_InvalidRequired()
        {
            var employee = new Employee();
            var validationErrors = employee.IsValid();

            Assert.NotNull(validationErrors);
            Assert.True(validationErrors.Any());
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

            var validationErrors = employee.IsValid();

            Assert.NotNull(validationErrors);
            Assert.True(validationErrors.Any());
        }
    }
}