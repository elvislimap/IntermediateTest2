using System;

namespace IntermediateTest2.Domain.Entities
{
    public class SharedFund
    {
        public int SharedFundId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Value { get; set; }
        public DateTime ContributionDate { get; set; }
    }
}