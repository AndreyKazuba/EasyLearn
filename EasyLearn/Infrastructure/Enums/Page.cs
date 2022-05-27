namespace EasyLearn.Infrastructure.Enums
{
    public enum Page
    {
        /// <summary>
        /// Страница проведения диктанта.
        /// </summary>
        Dictation = 0,

        /// <summary>
        /// Страница управления пользователями.
        /// </summary>
        Users = 1,

        /// <summary>
        /// Страница управления словарями.
        /// </summary>
        Dictionaries = 2,

        /// <summary>
        /// Страница редактирования простого словаря.
        /// </summary>
        EditCommonDictionaryPage = 3,

        /// <summary>
        /// Страница редактирования словаря предлог-глаголов.
        /// </summary>
        EditVerbPrepositionDictionaryPage = 4,
    }
}
