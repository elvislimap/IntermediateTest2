using System;

namespace IntermediateTest2.Domain.Commons
{
    public static class Extensions
    {
        public static string ToText(this object value)
        {
            return value != null ? value.ToString() : string.Empty;
        }

        public static int ToInt(this object value)
        {
            return string.IsNullOrEmpty(value.ToText())
                ? 0
                : int.TryParse(value.ToText(), out var valueInt)
                    ? valueInt
                    : 0;
        }

        public static decimal ToDecimal(this object value)
        {
            return value != null && value.ToText() != "" ? decimal.Parse(value.ToText()) : 0M;
        }

        public static string ToDateTimeEn(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}