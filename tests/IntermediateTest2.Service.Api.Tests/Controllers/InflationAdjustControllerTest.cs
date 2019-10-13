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
    public class InflationAdjustControllerTest
    {
        private readonly Mock<IInflationAdjustAppService> _inflationAdjustAppService;
        private InflationAdjustController _inflationAdjustController;

        public InflationAdjustControllerTest()
        {
            _inflationAdjustAppService = new Mock<IInflationAdjustAppService>();
        }


        [Fact]
        public async Task InflationAdjustController_Add()
        {
            var result = new Result(1);
            var inflationAdjust = new InflationAdjust { AdjustmentDate = DateTime.Now, Percentage = 1 };

            _inflationAdjustAppService.Setup(i => i.Add(inflationAdjust)).Returns(result);
            _inflationAdjustController = new InflationAdjustController(_inflationAdjustAppService.Object);

            var objectResult = await _inflationAdjustController.Add(inflationAdjust);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }

        [Fact]
        public async Task InflationAdjustController_GetAll()
        {
            var inflationAdjust = new InflationAdjust { AdjustmentDate = DateTime.Now, Percentage = 1 };
            var result = new Result(inflationAdjust);

            _inflationAdjustAppService.Setup(i => i.GetAll()).Returns(result);
            _inflationAdjustController = new InflationAdjustController(_inflationAdjustAppService.Object);

            var objectResult = await _inflationAdjustController.GetAll();

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }
    }
}