using System;
using System.Linq;

namespace EasyLearn.Infrastructure.Validation
{
    public static class ValidationHelper
    {
        private const string OneCharacter = "символ";
        private const string CharactersWithA = "символа";
        private const string CharactersWithOv = "символов";

        private static readonly int[] oneCharacterEntries = { 1 };
        private static readonly int[] сharactersWithAEntries = { 2, 3, 4 };
        public static string GetCharactersSubString(int charactresCount)
        {
            if (charactresCount < 0)
                throw new ArgumentOutOfRangeException(nameof(charactresCount));
            bool inSimpleRange = charactresCount >= 0 && charactresCount <= 20;
            while (!inSimpleRange)
            {
                charactresCount -= 10;
                inSimpleRange = charactresCount <= 20;
            }
            if (oneCharacterEntries.Any(number => number == charactresCount))
                return OneCharacter;
            else if (сharactersWithAEntries.Any(number => number == charactresCount))
                return CharactersWithA;
            else
                return CharactersWithOv;
        }
    }
}
