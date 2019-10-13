using System;
using System.Collections.Generic;
using System.Linq;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Infra.Data.Context;

namespace IntermediateTest2.Infra.Data.Repositories
{
    public class SharedFundRepository : ISharedFundRepository
    {
        private readonly ContextEf _context;

        public SharedFundRepository(ContextEf context)
        {
            _context = context;
        }


        public List<SharedFund> GetByEmployeeId(int employeeId)
        {
            return _context.SharedFunds
                .Where(s => s.EmployeeId == employeeId)
                .ToList();
        }

        public void Insert(SharedFund sharedFund)
        {
            _context.Add(sharedFund);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}