using IntermediateTest2.Domain.Entities;
using IntermediateTest2.Domain.Validations.Commons;
using IntermediateTest2.Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace IntermediateTest2.Domain.Validations
{
    public static class InflationAdjustValidation
    {
        public static bool IsValid(this InflationAdjust inflationAdjust, out List<ValidationError> validationErrors)
        {
            validationErrors = Required(inflationAdjust);
            return !validationErrors.Any();
        }

        private static List<ValidationError> Required(InflationAdjust inflationAdjust)
        {
            var validationErrors = new List<ValidationError>();

            if (!RequiredValidation.IsValid(inflationAdjust.Percentage))
                validationErrors.Add(ValidationError.Create("Percentage", "Percentage is required"));

            if (!RequiredValidation.IsValid(inflationAdjust.AdjustmentDate))
                validationErrors.Add(ValidationError.Create("AdjustmentDate", "AdjustmentDate is required"));

            return validationErrors;
        }
    }
}