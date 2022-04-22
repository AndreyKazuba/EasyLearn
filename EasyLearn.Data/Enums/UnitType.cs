using EasyLearn.Data.Attributes;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Enums
{
    public enum UnitType
    {
        /// <summary>
        /// Существительное
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Noun)]
        [EnglishTranslation(UnitTypeEnglishNames.Noun)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Noun = 1,

        /// <summary>
        /// Глагол
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Verb)]
        [EnglishTranslation(UnitTypeEnglishNames.Verb)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Verb = 2,

        /// <summary>
        /// Прилагательное
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Adjective)]
        [EnglishTranslation(UnitTypeEnglishNames.Adjective)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Adjective = 3,

        /// <summary>
        /// Модальный глагол
        /// </summary>
        [EnglishTranslation(UnitTypeEnglishNames.ModalVerb)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        ModalVerb = 4,

        /// <summary>
        /// Предлог
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Preposition)]
        [EnglishTranslation(UnitTypeEnglishNames.Preposition)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Preposition = 5,

        /// <summary>
        /// Предложение
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Sentence)]
        [EnglishTranslation(UnitTypeEnglishNames.Sentence)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Sentence = 6,

        /// <summary>
        /// Фраза
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Phrase)]
        [EnglishTranslation(UnitTypeEnglishNames.Phrase)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Phrase = 7,

        /// <summary>
        /// Первая форма неправильного глагола
        /// </summary>
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        IrregularV1 = 8,

        /// <summary>
        /// Вторая форма неправильного глагола
        /// </summary>
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        IrregularV2 = 9,

        /// <summary>
        /// Третья форма неправильного глагола
        /// </summary>
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        IrregularV3 = 10,

        /// <summary>
        /// Словосочетание
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.CombinationOfWords)]
        [EnglishTranslation(UnitTypeEnglishNames.CombinationOfWords)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        CombinationOfWords = 11,

        /// <summary>
        /// Союз
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Conjunction)]
        [EnglishTranslation(UnitTypeEnglishNames.Conjunction)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Conjunction = 12,

        /// <summary>
        /// Местоимение
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Pronoun)]
        [EnglishTranslation(UnitTypeEnglishNames.Pronoun)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Pronoun = 13,

        /// <summary>
        /// Числительное
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Numeral)]
        [EnglishTranslation(UnitTypeEnglishNames.Numeral)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Numeral = 14,

        /// <summary>
        /// Частица
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Particle)]
        [EnglishTranslation(UnitTypeEnglishNames.Particle)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Particle = 15,

        /// <summary>
        /// Наречие
        /// </summary>
        [RussianTranslaton(UnitTypeRussianNames.Adverb)]
        [EnglishTranslation(UnitTypeEnglishNames.Adverb)]
        [UnitTypeColorCode(UnitTypeColorCodes.SomeColor)]
        Adverb = 16,
    }
}
