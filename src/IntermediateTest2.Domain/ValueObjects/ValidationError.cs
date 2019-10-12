namespace IntermediateTest2.Domain.ValueObjects
{
    public class ValidationError
    {
        public string Field { get; set; }
        public string Message { get; set; }

        public static ValidationError Create(string field, string message)
        {
            return new ValidationError
            {
                Field = field,
                Message = message
            };
        }
    }
}