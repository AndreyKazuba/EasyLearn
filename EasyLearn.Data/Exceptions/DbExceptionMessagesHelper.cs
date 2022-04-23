namespace EasyLearn.Data.Exceptions
{
    public static class DbExceptionMessagesHelper
    {
        public static string AddingForNonExistingEntity(string addingEntityTypeName, string nonExistingEntityTypeName, string nonExistingEntityId) 
            => $"Попытка добавить {addingEntityTypeName} несуществующему {nonExistingEntityTypeName} с Id : '{nonExistingEntityId}'";
        public static string PropertyInvalidValue(string propertyName, string typeName, object value) 
            => $"Попытка задать в качестве {propertyName} для {typeName} невалидное значение: '{value.ToString()}'";
        public static string AttemptToAddExistingEntity(string existingEntityName, string firstPropName, string firstPropValue, string secondPropName, string secondPropValue)
            => $"Попытка добавить уже существующий {existingEntityName}: '{firstPropName} = {firstPropValue}, {secondPropName} = {secondPropValue}'";
        public static string AttemptToAddExistingEntity(string existingEntityName, string firstPropName, string firstPropValue, string secondPropName, string secondPropValue, string thirdPropName, string thirdPropValue)
            => $"Попытка добавить уже существующий {existingEntityName}: '{firstPropName} = {firstPropValue}, {secondPropName} = {secondPropValue}, {thirdPropName} = {thirdPropValue}'";
    }
}
