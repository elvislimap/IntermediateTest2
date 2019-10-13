using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.ValueObjects;

namespace IntermediateTest2.Application.Interfaces
{
    public interface IInflationAdjustAppService
    {
        Result Add(InflationAdjust inflationAdjust);
        Result GetAll();
    }
}