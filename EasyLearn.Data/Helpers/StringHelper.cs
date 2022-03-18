namespace EasyLearn.Data.Helpers
{
    public static class StringHelper
    {
        public static bool Equals(string x, string y)
        {
            return x.ToLower() == y.ToLower();
        }

        public static string PrepareToDb(string @string)
        {
            return @string.ToLower().Trim();
        }
    }
}
