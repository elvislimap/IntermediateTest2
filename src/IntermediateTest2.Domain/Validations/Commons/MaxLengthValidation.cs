namespace IntermediateTest2.Domain.Validations.Commons
{
    public abstract class MaxLengthValidation
    {
        public static bool IsValid(string value, int max)
        {
            return value != null && value.Length <= max;
        }
    }
}