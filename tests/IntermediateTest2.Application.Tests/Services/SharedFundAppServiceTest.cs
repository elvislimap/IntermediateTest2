using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Application.Services;
using IntermediateTest2.Domain.Commons;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;
using IntermediateTest2.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntermediateTest2.Application.Tests.Services
{
    public class SharedFundAppServiceTest
    {
        private readonly Mock<ISharedFundRepository> _sharedFundRepository;
        private readonly Mock<IInflationAdjustService> _inflationAdjustService;
        private readonly Mock<IEmployeeService> _employeeService;
        private ISharedFundAppService _sharedFundAppService;

        public SharedFundAppServiceTest()
        {
            _sharedFundRepository = new Mock<ISharedFundRepository>();
            _inflationAdjustService = new Mock<IInflationAdjustService>();
            _employeeService = new Mock<IEmployeeService>();
        }


        [Fact]
        public void SharedFundAppService_AddOk()
        {
            var sharedFund = new SharedFund
            {
                SharedFundId = 1,
                EmployeeId = 1,
                ContributionDate = DateTime.Now
            };

            var result = Add(sharedFund);

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(sharedFund.SharedFundId, result.Return.ToInt());
        }

        [Fact]
        public void SharedFundAppService_AddError()
        {
            var sharedFund = new SharedFund { EmployeeId = 1 };
            var result = Add(sharedFund);

            Assert.NotNull(result);
            Assert.False(result.MessageErrors.Any());
            Assert.True(result.ValidationErrors.Any());
            Assert.Null(result.Return);
        }

        [Fact]
        public void SharedFundAppService_GetBalance()
        {
            const int employeeId = 1;
            const decimal balance = 560M;
            var sharedFunds = new List<SharedFund> { GenerateSharedFund() };

            _sharedFundRepository.Setup(s => s.GetByEmployeeId(employeeId)).Returns(sharedFunds);
            _inflationAdjustService.Setup(i => i.InflationAdjustment(sharedFunds)).Returns(balance);

            _sharedFundAppService = new SharedFundAppService(_sharedFundRepository.Object,
                _inflationAdjustService.Object, _employeeService.Object);

            var result = _sharedFundAppService.GetBalance(employeeId);

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(balance, result.Return.ToDecimal());
        }

        [Fact]
        public void SharedFundAppService_Withdraw()
        {
            const int employeeId = 1;
            const decimal balance = 560M;
            var sharedFunds = new List<SharedFund> { GenerateSharedFund() };
            var ruleToWithdraw = RuleToWithdraw.GetRulesToWithdraw()
                .Where(r => r.MaxBalance >= balance)
                .OrderBy(r => r.MaxBalance)
                .FirstOrDefault();

            var percentage = ruleToWithdraw.Limit / 100M;
            var limitValue = balance * percentage;
            var toWithdraw = ruleToWithdraw.FixedMoney + limitValue;
            List<string> messagesErrors;

            _employeeService.Setup(e => e.CheckIfItBirthday(employeeId, out messagesErrors)).Returns(true);
            _sharedFundRepository.Setup(s => s.GetByEmployeeId(employeeId)).Returns(sharedFunds);
            _inflationAdjustService.Setup(i => i.InflationAdjustment(sharedFunds)).Returns(balance);
            _sharedFundRepository.Setup(s => s.Insert(It.IsAny<SharedFund>()));

            _sharedFundAppService = new SharedFundAppService(_sharedFundRepository.Object,
                _inflationAdjustService.Object, _employeeService.Object);

            var result = _sharedFundAppService.Withdraw(employeeId);

            Assert.NotNull(result);
            Assert.NotNull(result.Return);
            Assert.False(result.MessageErrors.Any());
            Assert.False(result.ValidationErrors.Any());
            Assert.Equal(toWithdraw, result.Return.ToDecimal());
        }


        private Result Add(SharedFund sharedFund)
        {
            _sharedFundRepository.Setup(s => s.Insert(sharedFund));
            _sharedFundAppService =
                new SharedFundAppService(_sharedFundRepository.Object,
                _inflationAdjustService.Object, _employeeService.Object);

            return _sharedFundAppService.Add(sharedFund);
        }

        private SharedFund GenerateSharedFund()
        {
            return new SharedFund
            {
                EmployeeId = 1,
                Value = 560M,
                ContributionDate = DateTime.Now
            };
        }
    }
}