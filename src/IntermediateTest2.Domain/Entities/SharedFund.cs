﻿using System;

namespace IntermediateTest2.Domain.Entities
{
    public class SharedFund
    {
        public SharedFund() { }

        public SharedFund(int employeeId, decimal value, DateTime contributionDate)
        {
            EmployeeId = employeeId;
            Value = value;
            ContributionDate = contributionDate;
        }

        public int SharedFundId { get; set; }
        public int EmployeeId { get; set; }
        public decimal Value { get; set; }
        public DateTime ContributionDate { get; set; }

        public Employee Employee { get; set; }
    }
}