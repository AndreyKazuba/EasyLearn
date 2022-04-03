using EasyLearn.Data.Enums;
using EasyLearn.Infrastructure.Constants;
using System;
using System.Windows.Media;

namespace EasyLearn.Infrastructure.Enums
{
    public static class EnumHelper
    {
        public static string GetRussianValue(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Noun: return UnitTypeRussianNames.Noun;
                case UnitType.Verb: return UnitTypeRussianNames.Verb;
                case UnitType.Adjective: return UnitTypeRussianNames.Adjective;
                case UnitType.Preposition: return UnitTypeRussianNames.Preposition;
                case UnitType.Sentence: return UnitTypeRussianNames.Sentence;
                case UnitType.Phrase: return UnitTypeRussianNames.Phrase;
                case UnitType.CombinationOfWords: return UnitTypeRussianNames.CombinationOfWords;
                case UnitType.Pronoun: return UnitTypeRussianNames.Pronoun;
                case UnitType.Numeral: return UnitTypeRussianNames.Numeral;
                case UnitType.Adverb: return UnitTypeRussianNames.Adverb;
                default: throw new Exception("Нет такого типа юнита");
            }
        }
        public static string GetEnglishValue(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Noun: return UnitTypeEnglishNames.Noun;
                case UnitType.Verb: return UnitTypeEnglishNames.Verb;
                case UnitType.Adjective: return UnitTypeEnglishNames.Adjective;
                case UnitType.Preposition: return UnitTypeEnglishNames.Preposition;
                case UnitType.Sentence: return UnitTypeEnglishNames.Sentence;
                case UnitType.Phrase: return UnitTypeEnglishNames.Phrase;
                case UnitType.CombinationOfWords: return UnitTypeEnglishNames.CombinationOfWords;
                case UnitType.Pronoun: return UnitTypeEnglishNames.Pronoun;
                case UnitType.Numeral: return UnitTypeEnglishNames.Numeral;
                case UnitType.Adverb: return UnitTypeEnglishNames.Adverb;
                case UnitType.ModalVerb: return UnitTypeEnglishNames.ModalVerb;
                default: throw new Exception("Нет такого типа юнита");
            }
        }
        public static Brush GetColor(this UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Noun: return Brushes.ForestGreen;
                case UnitType.Verb: return Brushes.DarkBlue;
                case UnitType.Adjective: return Brushes.Orange;
                case UnitType.Preposition: return Brushes.Black;
                case UnitType.Sentence: return Brushes.Black;
                case UnitType.Phrase: return Brushes.Black;
                case UnitType.CombinationOfWords: return Brushes.Red;
                case UnitType.Pronoun: return Brushes.Red;
                case UnitType.Numeral: return Brushes.Red;
                case UnitType.Adverb: return Brushes.Black;
                case UnitType.ModalVerb: return Brushes.Orange;
                default: throw new Exception("Нет такого типа юнита");
            }
        }
    }
}
