using EasyLearn.Data.Attributes;

namespace EasyLearn.Data.Enums
{
    public enum AnswerVariation
    {
        [AnswerSignificance(20)]
        FirstTry,

        [AnswerSignificance(-10)]
        SecondTry,

        [AnswerSignificance(-15)]
        ThirdTry,

        [AnswerSignificance(-20)]
        FourthPlusTry
    }
}
