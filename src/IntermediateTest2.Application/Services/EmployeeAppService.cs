using IntermediateTest2.Application.Interfaces;
using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Interfaces.Repositories;
using IntermediateTest2.Domain.Validations;
using IntermediateTest2.Domain.ValueObjects;

namespace IntermediateTest2.Application.Services
{
    public class EmployeeAppService : IEmployeeAppService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeAppService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public Result Add(Employee employee)
        {
            if (!employee.IsValid(out var validation))
                return new Result(validationErrors: validation);

            _employeeRepository.Insert(employee);

            return new Result(employee.EmployeeId);
        }

        public Result GetAll()
        {
            return new Result(_employeeRepository.GetAll());
        }
    }
}