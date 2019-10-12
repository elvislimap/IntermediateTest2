using IntermediateTest2.Domain.Commons;
using System;

namespace IntermediateTest2.Domain.Validations.Commons
{
    public abstract class RequiredValidation
    {
        public static bool IsValid(object value)
        {
            var valueText = value.ToText();

            if (int.TryParse(valueText, out var valueInt))
                return valueInt > 0;

            if (decimal.TryParse(valueText, out var valueDecimal))
                return valueDecimal > 0;

            if (DateTime.TryParse(valueText, out var valueDateTime))
                return valueDateTime > DateTime.MinValue && valueDateTime < DateTime.MaxValue;

            return !string.IsNullOrWhiteSpace(valueText);
        }
    }
}