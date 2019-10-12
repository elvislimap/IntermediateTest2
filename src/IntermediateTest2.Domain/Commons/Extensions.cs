namespace IntermediateTest2.Domain.Commons
{
    public static class Extensions
    {
        public static string ToText(this object value)
        {
            return value != null ? value.ToString() : string.Empty;
        }
    }
}