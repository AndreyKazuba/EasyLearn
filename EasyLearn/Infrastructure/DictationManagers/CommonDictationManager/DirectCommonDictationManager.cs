using System;
using System.Collections.Generic;
using System.Linq;
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
            selectedRelations = new List<CommonRelation>(UniversalHelper.Shuffle(allRelations).Take(dictationLength));
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
                    wrongAnswersCounter++;
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
    }
}
