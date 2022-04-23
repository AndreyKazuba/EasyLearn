using EasyLearn.Data.Attributes;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Enums
{
    public enum DictionaryType
    {
        /// <summary>
        /// Словарь неправильных глаголов
        /// </summary>
        [RussianTranslaton(DictionaryTypeRussianNames.IrregularVerbDictionary)]
        IrregularVerbDictionary = 0,

        /// <summary>
        /// Словарь глагол-предлогов
        /// </summary>
        [RussianTranslaton(DictionaryTypeRussianNames.VerbPrepositionDictionary)]
        VerbPrepositionDictionary = 1,

        /// <summary>
        /// Обычный словарь
        /// </summary>
        [RussianTranslaton(DictionaryTypeRussianNames.CommonDictionary)]
        CommonDictionary = 2,
    }
}
