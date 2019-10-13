using System;
using System.Collections.Generic;
using System.Linq;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Interfaces.Services;

namespace IntermediateTest2.Domain.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public bool CheckIfItBirthday(int employeeId, out List<string> messagesErrors)
        {
            var employee = _employeeRepository.Get(employeeId);
            var dateNow = DateTime.Now;

            messagesErrors = employee == null
                ? new List<string> { "Employee not found" }
                : !(employee.BirthDate.Day == dateNow.Day && employee.BirthDate.Month == dateNow.Month)
                    ? new List<string> { "Unable to cash out if it's not a birthday" }
                    : null;

            return messagesErrors == null;
        }

        public decimal GetValueBySalary(int employeeId, out List<string> messagesErrors)
        {
            var employee = _employeeRepository.Get(employeeId);

            messagesErrors = employee == null
                ? new List<string> { "Employee not found" }
                : new List<string>();

            var percentual = 8M / 100M;
            return messagesErrors.Any() ? 0 : employee.MonthlySalary * percentual;
        }
    }
}