using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations.Commons;
using IntermediateTest2.Domain.ValueObjects;
using System.Collections.Generic;

namespace IntermediateTest2.Domain.Validations
{
    public static class SharedFundValidation
    {
        public static List<ValidationError> IsValid(this SharedFund sharedFund)
        {
            return Required(sharedFund);
        }

        private static List<ValidationError> Required(SharedFund sharedFund)
        {
            var validationErrors = new List<ValidationError>();

            if (!RequiredValidation.IsValid(sharedFund.EmployeeId))
                validationErrors.Add(ValidationError.Create("EmployeeId", "EmployeeId is required"));

            if (!RequiredValidation.IsValid(sharedFund.Value))
                validationErrors.Add(ValidationError.Create("Value", "Value is required"));

            if (!RequiredValidation.IsValid(sharedFund.ContributionDate))
                validationErrors.Add(ValidationError.Create("ContributionDate", "ContributionDate is required"));

            return validationErrors;
        }
    }
}