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

#pragma warning disable CS8618
        public OppositeCommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > commonRelations.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            this.allRelations = commonRelations;
            this.selectedRelations = new List<CommonRelation>(UniversalHelper.Shuffle(allRelations).Take(dictationLength));
            this.maxCurrentRelationId = dictationLength - 1;
        }
#pragma warning restore CS8618

        #region Public methods
        public override bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            return this.AvailableRelations.Any(relation => StringHelper.Equals(relation.RussianUnit.Value, answer));
        }
        #endregion

        #region Proteched methods
        protected override void SetSynonymRelations()
        {
            int currentRelationEnglishUnitId = this.selectedRelations[currentRelationId].EnglishUnit.Id;
            this.synonymRelations = this.allRelations
                .Where(relation => relation.EnglishUnit.Id == currentRelationEnglishUnitId)
                .ToList();
        }
        #endregion
    }
}
