using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.ValueObjects;
using IntermediateTest2.Infra.Security.Interfaces;
using IntermediateTest2.Service.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace IntermediateTest2.Service.Api.Tests.Controllers
{
    public class SharedFundControllerTest
    {
        private readonly Mock<ISharedFundAppService> _sharedFundAppService;
        private SharedFundController _sharedFundController;

        public SharedFundControllerTest()
        {
            _sharedFundAppService = new Mock<ISharedFundAppService>();
        }


        [Fact]
        public async Task SharedFundController_Add()
        {
            var result = new Result(1);
            var sharedFund = new SharedFund { EmployeeId = 1, Value = 200M, ContributionDate = DateTime.Now };

            _sharedFundAppService.Setup(s => s.Add(sharedFund)).Returns(result);
            _sharedFundController = new SharedFundController(_sharedFundAppService.Object);

            var objectResult = await _sharedFundController.Add(sharedFund);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }

        [Fact]
        public async Task SharedFundController_GetBalance()
        {
            const int employeeId = 1;
            var sharedFunds = new List<SharedFund>{
                new SharedFund { EmployeeId = 1, Value = 200M, ContributionDate = DateTime.Now }
            };
            var result = new Result(sharedFunds);

            _sharedFundAppService.Setup(s => s.GetBalance(employeeId)).Returns(result);
            _sharedFundController = new SharedFundController(_sharedFundAppService.Object);

            var objectResult = await _sharedFundController.GetBalance(employeeId);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }

        [Fact]
        public async Task SharedFundController_Withdraw()
        {
            const int employeeId = 1;
            var result = new Result(500M);

            _sharedFundAppService.Setup(s => s.Withdraw(employeeId)).Returns(result);
            _sharedFundController = new SharedFundController(_sharedFundAppService.Object);

            var objectResult = await _sharedFundController.Withdraw(employeeId);

            Assert.NotNull(objectResult);
            Assert.Equal((int)HttpStatusCode.OK, objectResult.StatusCode);
            Assert.Equal(result.Return, ((Result)objectResult.Value).Return);
        }
    }
}