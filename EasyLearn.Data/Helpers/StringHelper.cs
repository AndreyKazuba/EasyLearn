namespace EasyLearn.Data.Helpers
{
    public static class StringHelper
    {
        public static bool Equals(string x, string y)
        {
            return x.ToUpper() == y.ToUpper();
        }
    }
}
