using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations;
using System;
using System.Linq;
using Xunit;

namespace IntermediateTest2.Domain.Tests.Entities
{
    public class InflationAdjustTest
    {
        [Fact]
        public void InflationAdjust_Valid()
        {
            var inflationAdjust = new InflationAdjust
            {
                Percentage = 0.02M,
                AdjustmentDate = DateTime.Now
            };

            var validationErrors = inflationAdjust.IsValid();

            Assert.NotNull(validationErrors);
            Assert.False(validationErrors.Any());
        }

        [Fact]
        public void InflationAdjust_InvalidRequired()
        {
            var inflationAdjust = new InflationAdjust();
            var validationErrors = inflationAdjust.IsValid();

            Assert.NotNull(validationErrors);
            Assert.True(validationErrors.Any());
            Assert.Equal(2, validationErrors.Count());
        }
    }
}