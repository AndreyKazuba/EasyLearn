using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Enums;
using EasyLearn.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

#pragma warning disable CS8618
namespace EasyLearn.Infrastructure.DictationManagers
{
    public abstract class CommonDictationManager
    {
        #region Private fields
        protected bool isStarted;
        protected int currentRelationId;
        protected int maxCurrentRelationId;
        protected List<CommonRelation> allRelations;
        protected List<CommonRelation> selectedRelations;
        protected List<CommonRelation> synonymRelations;
        #endregion

        #region Public props
        public CommonRelation CurrentRelation => selectedRelations[currentRelationId];
        public abstract string CurrentAnswerValue { get; }
        public List<CommonRelation> AvailableRelations => synonymRelations;
        public bool CurrentRelationHasSynonyms => synonymRelations.Count > 1;
        #endregion

        #region Public methods
        public CommonRelation Start()
        {
            ThrowIfItImpossibleToStart();
            isStarted = true;
            SetSynonymRelations();
            return selectedRelations[currentRelationId];
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
        public abstract bool IsAnswerCorrect(string answer);
        #endregion

        #region Protected methods
        protected abstract void SetSynonymRelations();
        protected void ThrowIfItImpossibleToStart()
        {
            if (selectedRelations is null || !selectedRelations.Any())
                throw new Exception(ExceptionMessagesHelper.CannotStartDictationWithoutWords);
        }
        protected void ThrowIfDictationIsNotStarted()
        {
            if (!isStarted)
                throw new Exception(ExceptionMessagesHelper.NeedsToStarDictationFirst);
        }
        #endregion

        #region Static methods
        public static CommonDictationManager CreateManager(List<CommonRelation> commonRelations, int dictationLength, DictationDirection direction)
        {
            switch (direction)
            {
                case DictationDirection.Directly:
                    return new DirectCommonDictationManager(commonRelations, dictationLength);
                case DictationDirection.Opposite:
                    return new OppositeCommonDictationManager(commonRelations, dictationLength);
                default: 
                    throw new Exception(ExceptionMessagesHelper.ThereIdNoSuchDictationDirection);
            }
        }
        #endregion
    }
}
