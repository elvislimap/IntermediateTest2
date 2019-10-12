using System;

namespace IntermediateTest2.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal MonthlySalary { get; set; }
    }
}