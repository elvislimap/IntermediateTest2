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
    public class InflationAdjustServiceTest
    {
        private readonly Mock<IInflationAdjustRepository> _inflationAdjustRepository;
        private IInflationAdjustService _inflationAdjustService;

        public InflationAdjustServiceTest()
        {
            _inflationAdjustRepository = new Mock<IInflationAdjustRepository>();
        }

        [Fact]
        public void InflationAdjustService_InflationAdjustment()
        {
            var inflationAdjusts = GenerateInflationAdjusts();
            var sharedFunds = GenerateSharedFunds();

            _inflationAdjustRepository.Setup(i => i.GetAll()).Returns(inflationAdjusts);
            _inflationAdjustService = new InflationAdjustService(_inflationAdjustRepository.Object);

            Assert.Equal(5606.28M, _inflationAdjustService.InflationAdjustment(sharedFunds));
        }


        private List<InflationAdjust> GenerateInflationAdjusts()
        {
            return new List<InflationAdjust> {
                new InflationAdjust
                {
                    InflationAdjustId = 1,
                    Percentage = 0.02M,
                    AdjustmentDate = new DateTime(2019, 3, 1)
                },
                new InflationAdjust
                {
                    InflationAdjustId = 2,
                    Percentage = 0.03M,
                    AdjustmentDate = new DateTime(2019, 7, 20)
                }
            };
        }

        private List<SharedFund> GenerateSharedFunds()
        {
            return new List<SharedFund>
            {
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 1, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 2, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 3, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 4, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 5, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 6, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 7, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 8, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 9, 10)
                },
                new SharedFund
                {
                    Value = 560,
                    ContributionDate = new DateTime(2019, 10, 10)
                }
            };
        }
    }
}