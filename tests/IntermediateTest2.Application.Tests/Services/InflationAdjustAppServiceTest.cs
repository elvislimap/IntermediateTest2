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
    public class InflationAdjustAppServiceTest
    {
        private readonly Mock<IInflationAdjustRepository> _inflationAdjustRepository;
        private IInflationAdjustAppService _inflationAdjustAppService;

        public InflationAdjustAppServiceTest()
        {
            _inflationAdjustRepository = new Mock<IInflationAdjustRepository>();
        }


        [Fact]
        public void InflationAdjustAppService_AddOk()
        {
            var inflationAdjust = GenerateInflationAdjustOk();
            var result = Add(inflationAdjust);

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(inflationAdjust.InflationAdjustId, result.Return.ToInt());
        }

        [Fact]
        public void InflationAdjustAppService_AddError()
        {
            var inflationAdjust = new InflationAdjust { AdjustmentDate = DateTime.Now };
            var result = Add(inflationAdjust);

            Assert.NotNull(result);
            Assert.False(result.MessageErrors.Any());
            Assert.True(result.ValidationErrors.Any());
            Assert.Null(result.Return);
        }

        [Fact]
        public void InflationAdjustAppService_GetAll()
        {
            var inflationAdjusts = new List<InflationAdjust> { GenerateInflationAdjustOk() };

            _inflationAdjustRepository.Setup(i => i.GetAll()).Returns(inflationAdjusts);
            _inflationAdjustAppService = new InflationAdjustAppService(_inflationAdjustRepository.Object);

            var result = _inflationAdjustAppService.GetAll();

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(inflationAdjusts, (List<InflationAdjust>)result.Return);
        }


        private Result Add(InflationAdjust inflationAdjust)
        {
            _inflationAdjustRepository.Setup(i => i.Insert(inflationAdjust));
            _inflationAdjustAppService = new InflationAdjustAppService(_inflationAdjustRepository.Object);

            return _inflationAdjustAppService.Add(inflationAdjust);
        }

        private InflationAdjust GenerateInflationAdjustOk()
        {
            return new InflationAdjust
            {
                Percentage = 0.02M,
                AdjustmentDate = DateTime.Now
            };
        }
    }
}