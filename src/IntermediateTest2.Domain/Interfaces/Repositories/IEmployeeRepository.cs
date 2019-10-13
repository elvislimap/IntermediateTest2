using IntermediateTest2.Domain.Entities;
using System;
using System.Collections.Generic;

namespace IntermediateTest2.Domain.Interfaces.Repositories
{
    public interface IEmployeeRepository : IDisposable
    {
        void Insert(Employee employee);
        List<Employee> GetAll();
        Employee Get(int employeeId);
    }
}