using System.Collections.Generic;

namespace IntermediateTest2.Domain.Interfaces.Services
{
    public interface IEmployeeService
    {
        bool CheckIfItBirthday(int employeeId, out List<string> messagesErrors);
        decimal GetValueBySalary(int employeeId, out List<string> messagesErrors);
    }
}