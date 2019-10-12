using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations.Commons;
using IntermediateTest2.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IntermediateTest2.Domain.Validations
{
    public static class EmployeeValidation
    {
        public static List<ValidationError> IsValid(this Employee employee)
        {
            var requiredValidation = Required(employee);

            return requiredValidation.Any() ? requiredValidation : Valid(employee);
        }

        private static List<ValidationError> Required(Employee employee)
        {
            var validationErrors = new List<ValidationError>();

            if (!RequiredValidation.IsValid(employee.Name))
                validationErrors.Add(ValidationError.Create("Name", "Name is required"));

            if (!RequiredValidation.IsValid(employee.BirthDate))
                validationErrors.Add(ValidationError.Create("BirthDate", "BirthDate is required"));

            if (!RequiredValidation.IsValid(employee.MonthlySalary))
                validationErrors.Add(ValidationError.Create("MonthlySalary", "MonthlySalary is required"));

            return validationErrors;
        }

        private static List<ValidationError> Valid(Employee employee)
        {
            const int maxName = 100;
            var validationErrors = new List<ValidationError>();

            if (!MaxLengthValidation.IsValid(employee.Name, maxName))
                validationErrors.Add(ValidationError.Create("Name", $"Name must contain a maximum of {maxName} characters"));

            return validationErrors;
        }
    }
}