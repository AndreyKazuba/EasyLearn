using EasyLearn.Data.Attributes;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Enums
{
    public enum UnitType
    {
        /// <summary>
        /// Существительное
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Noun)]
        [EnglishTranslation(UnitTypeEnglishNames.Noun)]
        [UnitTypeColorCode(UnitTypeColorCodes.Noun)]
        Noun = 1,

        /// <summary>
        /// Глагол
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Verb)]
        [EnglishTranslation(UnitTypeEnglishNames.Verb)]
        [UnitTypeColorCode(UnitTypeColorCodes.Verb)]
        Verb = 2,

        /// <summary>
        /// Прилагательное
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Adjective)]
        [EnglishTranslation(UnitTypeEnglishNames.Adjective)]
        [UnitTypeColorCode(UnitTypeColorCodes.Adjective)]
        Adjective = 3,

        /// <summary>
        /// Модальный глагол
        /// </summary>
        [EnglishTranslation(UnitTypeEnglishNames.ModalVerb)]
        [RussianTranslation(UnitTypeRussianNames.ModalVerb)]
        [UnitTypeColorCode(UnitTypeColorCodes.ModalVerb)]
        ModalVerb = 4,

        /// <summary>
        /// Предлог
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Preposition)]
        [EnglishTranslation(UnitTypeEnglishNames.Preposition)]
        [UnitTypeColorCode(UnitTypeColorCodes.Preposition)]
        Preposition = 5,

        /// <summary>
        /// Предложение
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Sentence)]
        [EnglishTranslation(UnitTypeEnglishNames.Sentence)]
        [UnitTypeColorCode(UnitTypeColorCodes.Sentence)]
        Sentence = 6,

        /// <summary>
        /// Фраза
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Phrase)]
        [EnglishTranslation(UnitTypeEnglishNames.Phrase)]
        [UnitTypeColorCode(UnitTypeColorCodes.Phrase)]
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
        [RussianTranslation(UnitTypeRussianNames.CombinationOfWords)]
        [EnglishTranslation(UnitTypeEnglishNames.CombinationOfWords)]
        [UnitTypeColorCode(UnitTypeColorCodes.CombinationOfWords)]
        CombinationOfWords = 11,

        /// <summary>
        /// Союз
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Conjunction)]
        [EnglishTranslation(UnitTypeEnglishNames.Conjunction)]
        [UnitTypeColorCode(UnitTypeColorCodes.Conjunction)]
        Conjunction = 12,

        /// <summary>
        /// Местоимение
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Pronoun)]
        [EnglishTranslation(UnitTypeEnglishNames.Pronoun)]
        [UnitTypeColorCode(UnitTypeColorCodes.Pronoun)]
        Pronoun = 13,

        /// <summary>
        /// Числительное
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Numeral)]
        [EnglishTranslation(UnitTypeEnglishNames.Numeral)]
        [UnitTypeColorCode(UnitTypeColorCodes.Numeral)]
        Numeral = 14,

        /// <summary>
        /// Частица
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Particle)]
        [EnglishTranslation(UnitTypeEnglishNames.Particle)]
        [UnitTypeColorCode(UnitTypeColorCodes.Particle)]
        Particle = 15,

        /// <summary>
        /// Наречие
        /// </summary>
        [RussianTranslation(UnitTypeRussianNames.Adverb)]
        [EnglishTranslation(UnitTypeEnglishNames.Adverb)]
        [UnitTypeColorCode(UnitTypeColorCodes.Adverb)]
        Adverb = 16,
    }
}
