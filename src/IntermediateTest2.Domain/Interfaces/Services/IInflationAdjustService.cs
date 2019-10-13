using IntermediateTest2.Domain.Entities;
using System.Collections.Generic;

namespace IntermediateTest2.Domain.Interfaces.Services
{
    public interface IInflationAdjustService
    {
        decimal InflationAdjustment(List<SharedFund> sharedFunds);
    }
}