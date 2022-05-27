using System;
using System.Collections.Generic;
using System.Linq;
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
            this.allRelations = commonRelations;
            this.selectedRelations = new List<CommonRelation>(UniversalHelper.Shuffle(allRelations).Take(dictationLength));
            this.maxCurrentRelationId = dictationLength - 1;
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
                    wrongAnswersCounter++;
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
    }
}
