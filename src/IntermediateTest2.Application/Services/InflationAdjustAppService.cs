using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Validations;
using IntermediateTest2.Domain.ValueObjects;

namespace IntermediateTest2.Application.Services
{
    public class InflationAdjustAppService : IInflationAdjustAppService
    {
        private readonly IInflationAdjustRepository _inflationAdjustRepository;

        public InflationAdjustAppService(IInflationAdjustRepository inflationAdjustRepository)
        {
            _inflationAdjustRepository = inflationAdjustRepository;
        }


        public Result Add(InflationAdjust inflationAdjust)
        {
            if (!inflationAdjust.IsValid(out var validation))
                return new Result(validationErrors: validation);

            _inflationAdjustRepository.Insert(inflationAdjust);

            return new Result(inflationAdjust.InflationAdjustId);
        }

        public Result GetAll()
        {
            return new Result(_inflationAdjustRepository.GetAll());
        }
    }
}