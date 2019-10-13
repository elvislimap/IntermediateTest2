using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;
using IntermediateTest2.Domain.Validations;
using IntermediateTest2.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntermediateTest2.Application.Services
{
    public class SharedFundAppService : ISharedFundAppService
    {
        private readonly ISharedFundRepository _sharedFundRepository;
        private readonly IInflationAdjustService _inflationAdjustService;
        private readonly IEmployeeService _employeeService;

        public SharedFundAppService(ISharedFundRepository sharedFundRepository,
            IInflationAdjustService inflationAdjustService, IEmployeeService employeeService)
        {
            _sharedFundRepository = sharedFundRepository;
            _inflationAdjustService = inflationAdjustService;
            _employeeService = employeeService;
        }


        public Result Add(SharedFund sharedFund)
        {
            if (!sharedFund.IsValid(out var validation))
                return new Result(validationErrors: validation);

            sharedFund.Value = _employeeService.GetValueBySalary(sharedFund.EmployeeId, out var messagesErrors);
            if (messagesErrors != null && messagesErrors.Any())
                return new Result(messages: messagesErrors);

            _sharedFundRepository.Insert(sharedFund);

            return new Result(sharedFund.SharedFundId);
        }

        public Result GetBalance(int employeeId)
        {
            return new Result(Balance(employeeId, out var _));
        }

        public Result Withdraw(int employeeId)
        {
            if (!_employeeService.CheckIfItBirthday(employeeId, out var messagesErrors))
                return new Result(messages: messagesErrors);

            var balance = Balance(employeeId, out var releasedToCashOut);
            if (!releasedToCashOut)
                return new Result(messages: new List<string> { "Cashout not available" });

            var toWithdraw = ToWithdraw(balance);
            var shareFund = new SharedFund(employeeId, toWithdraw * -1, DateTime.Now);

            _sharedFundRepository.Insert(shareFund);

            return new Result(toWithdraw);
        }


        private decimal Balance(int employeeId, out bool releasedToCashOut)
        {
            var sharedFunds = _sharedFundRepository.GetByEmployeeId(employeeId);
            releasedToCashOut = !sharedFunds.Any(s => s.Value < 0 && s.ContributionDate.Date == DateTime.Now.Date);

            return _inflationAdjustService.InflationAdjustment(sharedFunds);
        }

        private static decimal ToWithdraw(decimal balance)
        {
            var ruleToWithdraw = RuleToWithdraw.GetRulesToWithdraw()
                .Where(r => r.MaxBalance >= balance)
                .OrderBy(r => r.MaxBalance)
                .FirstOrDefault();

            var percentage = ruleToWithdraw.Limit / 100M;
            var limitValue = Math.Round(balance * percentage, 2);

            return ruleToWithdraw.FixedMoney + limitValue;
        }
    }
}