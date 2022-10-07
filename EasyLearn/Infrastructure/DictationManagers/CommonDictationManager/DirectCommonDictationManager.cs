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
    public class DirectCommonDictationManager : CommonDictationManager
    {
        #region Public props
        public override string CurrentAnswerValue => CurrentRelation.EnglishUnit.Value;
        #endregion

        public DirectCommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > commonRelations.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            allRelations = commonRelations;
            selectedRelations = SelectRelations(commonRelations, dictationLength);
            maxCurrentRelationId = dictationLength - 1;
        }

        #region Public methods
        public override bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = SynonymRelations.Any(relation => StringHelper.Equals(relation.EnglishUnit.Value, answer));
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
            int currentRelationRussianUnitId = selectedRelations[currentRelationId].RussianUnit.Id;
            synonymRelations = allRelations
                .Where(relation => relation.RussianUnit.Id == currentRelationRussianUnitId)
                .ToList();
        }
        #endregion

        #region Private methods
        private int GetSynonymId(string answer) => SynonymRelations.First(relation => StringHelper.Equals(relation.EnglishUnit.Value, answer)).Id;
        #endregion
    }
}
