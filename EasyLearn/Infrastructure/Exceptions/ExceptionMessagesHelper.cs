namespace EasyLearn.Infrastructure.Exceptions
{
    public static class ExceptionMessagesHelper
    {
        public const string NeedsToStarDictationFirst = "Сначала нужно стартовать диктант";
        public const string CannotStartDictationWithoutWords = "Нельзя начать диктант без слов";
        public const string NoSuchDictationType = "Нет такого типа диктанта";
        public const string FailedToGetCurrentUserId = "Не удалось получить из базы Id текущего пользователя";
        public const string ThereIsNoSuchService = "Нет такого сервиса";
        public const string ThereIdNoSuchDictationDirection = "Нет такого направления диктанта";
        public const string DictationManagerIsNull = "Незвозможно выполнить действие без предварительного создания менеджера";
        public const string EnumElementNotImplementedInSwitch = "Данный элемент перечисления не предусмотрен";
        public static string NoSuchDictionaryOnUI(string dictionaryTypeName, int id) => $"На UI нет {dictionaryTypeName} с Id = {id.ToString()}";
        public static string InvalidArgumentType(string argName, string argNeededTypeName) => $"Аргумент {argName} должен быть типа {argNeededTypeName}";
    }
}
