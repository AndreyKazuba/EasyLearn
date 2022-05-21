using EasyLearn.Data.Attributes;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Enums
{
    public enum DictionaryType
    {
        /// <summary>
        /// Словарь неправильных глаголов
        /// </summary>
        [RussianTranslation(DictionaryTypeRussianNames.IrregularVerbDictionary)]
        IrregularVerbDictionary = 0,

        /// <summary>
        /// Словарь глагол-предлогов
        /// </summary>
        [RussianTranslation(DictionaryTypeRussianNames.VerbPrepositionDictionary)]
        VerbPrepositionDictionary = 1,

        /// <summary>
        /// Обычный словарь
        /// </summary>
        [RussianTranslation(DictionaryTypeRussianNames.CommonDictionary)]
        CommonDictionary = 2,
    }
}
