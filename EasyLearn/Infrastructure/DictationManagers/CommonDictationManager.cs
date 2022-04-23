using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class CommonDictationManager
    {
        #region Private fields
        private bool isStarted;
        private int currentRelationId;
        private int maxCurrentRelationId;
        private List<CommonRelation> allRelations;
        private List<CommonRelation> selectedRelations;
        private List<CommonRelation> synonymRelations;
        #endregion

        #region Public props
        public CommonRelation CurrentRelation => selectedRelations[currentRelationId];
        public string CurrentRussianValue => CurrentRelation.RussianUnit.Value;
        public string CurrentEnglishValue => CurrentRelation.EnglishUnit.Value;
        public List<CommonRelation> AvailableRelations => synonymRelations;
        public bool CurrentRelationHasSynonyms => synonymRelations.Count > 1;
        #endregion

#pragma warning disable CS8618
        public CommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > commonRelations.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            this.allRelations = commonRelations;
            this.selectedRelations = new List<CommonRelation>(UniversalHelper.Shuffle(allRelations).Take(dictationLength));
            this.maxCurrentRelationId = dictationLength - 1;
        }
#pragma warning restore CS8618

        #region Public methods
        public CommonRelation Start()
        {
            ThrowIfItImpossibleToStart();
            this.isStarted = true;
            SetSynonymRelations();
            return this.selectedRelations[currentRelationId];
        }
        public bool GoNext()
        {
            ThrowIfDictationIsNotStarted();
            if (++currentRelationId <= maxCurrentRelationId)
            {
                SetSynonymRelations();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            return this.AvailableRelations.Any(relation => StringHelper.Equals(relation.EnglishUnit.Value, answer));
        }
        #endregion

        #region Private methods
        private void SetSynonymRelations()
        {
            int currentRelationRussianUnitId = this.selectedRelations[currentRelationId].RussianUnit.Id;
            this.synonymRelations = this.allRelations
                .Where(relation => relation.RussianUnit.Id == currentRelationRussianUnitId)
                .ToList();
        }
        private void ThrowIfItImpossibleToStart()
        {
            if (this.selectedRelations is null || !this.selectedRelations.Any())
                throw new Exception(ExceptionMessagesHelper.CannotStartDictationWithoutWords);
        }
        private void ThrowIfDictationIsNotStarted()
        {
            if (!this.isStarted)
                throw new Exception(ExceptionMessagesHelper.NeedsToStarDictationFirst);
        }
        #endregion
    }
}
