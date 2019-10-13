using System;
using System.Collections.Generic;
using System.Linq;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Infra.Data.Context;

namespace IntermediateTest2.Infra.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ContextEf _context;

        public EmployeeRepository(ContextEf context)
        {
            _context = context;
        }


        public void Insert(Employee employee)
        {
            _context.Add(employee);
            _context.SaveChanges();
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee Get(int employeeId)
        {
            return _context.Employees.Find(employeeId);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}