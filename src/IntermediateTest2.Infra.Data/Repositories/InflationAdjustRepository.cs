using System;
using System.Collections.Generic;
using System.Linq;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Infra.Data.Context;

namespace IntermediateTest2.Infra.Data.Repositories
{
    public class InflationAdjustRepository : IInflationAdjustRepository
    {
        private readonly ContextEf _context;

        public InflationAdjustRepository(ContextEf context)
        {
            _context = context;
        }


        public List<InflationAdjust> GetAll()
        {
            return _context.InflationAdjusts.ToList();
        }

        public void Insert(InflationAdjust inflationAdjust)
        {
            _context.Add(inflationAdjust);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}