using System;
using System.Collections.Generic;
using System.Linq;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;

namespace IntermediateTest2.Domain.Services
{
    public class InflationAdjustService : IInflationAdjustService
    {
        private readonly IInflationAdjustRepository _inflationAdjustRepository;

        public InflationAdjustService(IInflationAdjustRepository inflationAdjustRepository)
        {
            _inflationAdjustRepository = inflationAdjustRepository;
        }


        public decimal InflationAdjustment(List<SharedFund> sharedFunds)
        {
            var balance = 0M;
            var inflationAdjusts = _inflationAdjustRepository.GetAll();

            foreach (var sharedFund in sharedFunds)
            {
                var percentage = GetInflationByContributionDate(inflationAdjusts, sharedFund.ContributionDate);

                balance += sharedFund.Value < 0 ? 0 : Math.Round(balance * (percentage / 100), 2);
                balance += sharedFund.Value;
            }

            return balance;
        }

        private static decimal GetInflationByContributionDate(List<InflationAdjust> inflationAdjusts,
            DateTime contributionDate)
        {
            return inflationAdjusts
                .Where(i => i.AdjustmentDate <= contributionDate)
                .OrderBy(i => i.InflationAdjustId)
                .LastOrDefault()?.Percentage ?? 0;
        }
    }
}
