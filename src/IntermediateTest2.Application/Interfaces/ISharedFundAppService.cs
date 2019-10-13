using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.ValueObjects;

namespace IntermediateTest2.Application.Interfaces
{
    public interface ISharedFundAppService
    {
        Result Add(SharedFund sharedFund);
        Result GetBalance(int employeeId);
        Result Withdraw(int employeeId);
    }
}