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
                ContributionDate = DateTime.Now
            };

            Assert.True(sharedFund.IsValid(out var validationErrors));
            Assert.NotNull(validationErrors);
        }

        [Fact]
        public void SharedFund_InvalidRequired()
        {
            var sharedFund = new SharedFund();

            Assert.False(sharedFund.IsValid(out var validationErrors));
            Assert.NotNull(validationErrors);
            Assert.Equal(2, validationErrors.Count());
        }
    }
}