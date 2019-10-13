using System.Collections.Generic;

namespace IntermediateTest2.Domain.ValueObjects
{
    public class RuleToWithdraw
    {
        public RuleToWithdraw(decimal minBalance, decimal maxBalance, int limit, decimal fixedMoney)
        {
            MinBalance = minBalance;
            MaxBalance = maxBalance;
            Limit = limit;
            FixedMoney = fixedMoney;
        }

        public decimal MinBalance { get; set; }
        public decimal MaxBalance { get; set; }
        public int Limit { get; set; }
        public decimal FixedMoney { get; set; }

        public static List<RuleToWithdraw> GetRulesToWithdraw()
        {
            return new List<RuleToWithdraw>
            {
                new RuleToWithdraw(0, 500M, 50, 0),
                new RuleToWithdraw(500.01M, 1000M, 40, 50M),
                new RuleToWithdraw(1000.01M, 5000M, 30, 150M),
                new RuleToWithdraw(5000.01M, 10000M, 20, 650M),
                new RuleToWithdraw(10000.01M, 15000M, 15, 1150M),
                new RuleToWithdraw(15000.01M, 20000M, 10, 1900M),
                new RuleToWithdraw(20000.01M, decimal.MaxValue, 5, 2900M)
            };
        }
    }
}