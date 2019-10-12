using System.Collections.Generic;

namespace IntermediateTest2.Domain.ValueObjects
{
    public class Result
    {
        public Result(object ret = null, List<string> messages = null, List<ValidationError> validationErrors = null)
        {
            Return = ret;
            MessageErrors = messages;
            ValidationErrors = validationErrors;
        }

        public object Return { get; set; }
        public List<string> MessageErrors { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
    }
}