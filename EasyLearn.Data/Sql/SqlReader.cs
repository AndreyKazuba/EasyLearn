using System.IO;

namespace EasyLearn.Data.Sql
{
    public static class SqlReader
    {
        public static string GetSql(string fileName)
        {
            return File.ReadAllText($"D:\\EasyLearn\\EasyLearn.Data\\Sql\\{fileName}.sql");
        }
    }
}
