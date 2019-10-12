using System;

namespace IntermediateTest2.Domain.Entities
{
    public class InflationAdjust
    {
        public int InflationAdjustId { get; set; }
        public decimal Percentage { get; set; }
        public DateTime AdjustmentDate { get; set; }
    }
}