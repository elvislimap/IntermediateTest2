using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations;
using System;
using System.Linq;
using Xunit;

namespace IntermediateTest2.Domain.Tests.Entities
{
    public class SharedFundTest
    {
        [Fact]
        public void SharedFund_Valid()
        {
            var sharedFund = new SharedFund
            {
                EmployeeId = 1,
                Value = 560M,
                ContributionDate = DateTime.Now
            };

            var validationErrors = sharedFund.IsValid();

            Assert.NotNull(validationErrors);
            Assert.False(validationErrors.Any());
        }

        [Fact]
        public void SharedFund_InvalidRequired()
        {
            var sharedFund = new SharedFund();
            var validationErrors = sharedFund.IsValid();

            Assert.NotNull(validationErrors);
            Assert.True(validationErrors.Any());
            Assert.Equal(3, validationErrors.Count());
        }
    }
}