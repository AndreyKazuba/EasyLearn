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

#pragma warning disable CS8618
        public DirectCommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
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
            return this.AvailableRelations.Any(relation => StringHelper.Equals(relation.EnglishUnit.Value, answer));
        }
        #endregion

        #region Proteched methods
        protected override void SetSynonymRelations()
        {
            int currentRelationRussianUnitId = this.selectedRelations[currentRelationId].RussianUnit.Id;
            this.synonymRelations = this.allRelations
                .Where(relation => relation.RussianUnit.Id == currentRelationRussianUnitId)
                .ToList();
        }
        #endregion
    }
}
