using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.ValueObjects;

namespace IntermediateTest2.Application.Interfaces
{
    public interface IEmployeeAppService
    {
        Result Add(Employee employee);
        Result GetAll();
    }
}