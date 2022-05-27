using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.DTO;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class OppositeCommonDictationManager : CommonDictationManager
    {
        #region Public props
        public override string CurrentAnswerValue => CurrentRelation.RussianUnit.Value;
        #endregion

        public OppositeCommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > commonRelations.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            allRelations = commonRelations;
            selectedRelations = new List<CommonRelation>(allRelations.OrderBy(commonRelation => commonRelation.Priotiry).Take(dictationLength).Shuffle());
            maxCurrentRelationId = dictationLength - 1;
        }

        #region Public methods
        public override bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = SynonymRelations.Any(relation => StringHelper.Equals(relation.RussianUnit.Value, answer));
            if (currentAnswerIsNew)
            {
                answersCounter++;
                currentAnswerIsNew = false;
                if (!answerIsCorrect)
                {
                    wrongAnswersCounter++;
                    answers.Add(new Answer { RelationId = selectedRelations[currentRelationId].Id, Variation = AnswerVariation.SecondTry });
                }
                else
                {
                    answers.Add(new Answer { RelationId = GetSynonymId(answer), Variation = AnswerVariation.FirstTry });
                }
            }
            else if (!answerIsCorrect)
            {
                Answer lastAnswer = answers.Last();
                lastAnswer.Variation = lastAnswer.Variation == AnswerVariation.SecondTry ? AnswerVariation.ThirdTry : AnswerVariation.FourthPlusTry;
            }
            return answerIsCorrect;
        }
        #endregion

        #region Protected methods
        protected override void SetSynonymRelations()
        {
            int currentRelationEnglishUnitId = selectedRelations[currentRelationId].EnglishUnit.Id;
            synonymRelations = allRelations
                .Where(relation => relation.EnglishUnit.Id == currentRelationEnglishUnitId)
                .ToList();
        }
        #endregion

        #region Private methods
        private int GetSynonymId(string answer) => SynonymRelations.First(relation => StringHelper.Equals(relation.RussianUnit.Value, answer)).Id;
        #endregion
    }
}
