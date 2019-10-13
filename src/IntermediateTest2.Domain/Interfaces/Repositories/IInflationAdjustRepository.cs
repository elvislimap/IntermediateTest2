using IntermediateTest2.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IntermediateTest2.Domain.Interfaces.Repositories
{
    public interface IInflationAdjustRepository : IDisposable
    {
        void Insert(InflationAdjust inflationAdjust);
        List<InflationAdjust> GetAll();
    }
}