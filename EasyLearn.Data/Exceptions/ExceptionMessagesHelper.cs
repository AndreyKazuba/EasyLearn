namespace EasyLearn.Data.Exceptions
{
    public static class ExceptionMessagesHelper
    {
        public static string PropertyInvalidValue(string propertyName, string typeName, object value) => $"Попытка задать в качестве {propertyName} для {typeName} невалидное значение: '{value.ToString()}'";
    }
}
