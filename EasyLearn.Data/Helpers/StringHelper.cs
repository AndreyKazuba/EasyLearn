using System;
using System.Text.RegularExpressions;

namespace EasyLearn.Data.Helpers
{
    public static class StringHelper
    {
        public static bool Equals(string x, string y) => Prepare(x) == Prepare(y);
        public static string Prepare(this string @string) => @string.ToLower().ReduceSpaces().Trim();
        public static string? TryPrepare(this string? @string) => @string?.ToLower().ReduceSpaces().Trim();
        public static bool IsEmptyOrWhiteSpace(this string @string)
        {
            Regex regex = new Regex(@"^ +$");
            return @string == string.Empty || regex.IsMatch(@string);
        }
        public static string ReduceSpaces(this string @string)
        {
            Regex regex = new Regex(@" +");
            return regex.Replace(@string, " ");
        }
        public static string NormalizeRegister(this string @string) => @string.Substring(0, 1).ToUpper() + @string.Remove(0, 1).ToLower();
        public static string TryNormalizeRegister(this string? @string) => @string?.Substring(0, 1).ToUpper() + @string?.Remove(0, 1).ToLower();
        public static string PrepareAndNormalize(this string @string) => @string.Prepare().NormalizeRegister();
        public static string? NullIfEmptyOrWhiteSpace(this string @string) => @string is null || IsEmptyOrWhiteSpace(@string) ? null : @string;
        public static string EmptyIfNull(this string? @string) => @string is null ? String.Empty : @string;
    }
}
