using IntermediateTest2.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IntermediateTest2.Domain.Interfaces.Repositories
{
    public interface ISharedFundRepository : IDisposable
    {
        void Insert(SharedFund sharedFund);
        List<SharedFund> GetByEmployeeId(int employeeId);
    }
}