using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Exceptions;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class VerbPrepositionDictationManager
    {
        #region Private fields
        private bool isStarted;
        private int currentVerbPrepositionId;
        private int maxCurrentVerbPrepositionId;
        private List<VerbPreposition> verbPrepositions;
        #endregion

        #region Public props
        public VerbPreposition CurrentVerbPreposition => verbPrepositions[currentVerbPrepositionId];
        public string CurrentPrepositionValue => CurrentVerbPreposition.Preposition.Value;
        public string CurrentVerbValue => CurrentVerbPreposition.Verb.Value;
        #endregion

        public VerbPrepositionDictationManager(List<VerbPreposition> verbPrepositions)
        {
            this.verbPrepositions = verbPrepositions;
            this.maxCurrentVerbPrepositionId = verbPrepositions.Count - 1;
        }

        #region Public methods
        public VerbPreposition Start()
        {
            ThrowIfItImpossibleToStart();
            this.isStarted = true;
            return this.verbPrepositions[currentVerbPrepositionId];
        }
        public bool GoNext()
        {
            ThrowIfDictationIsNotStarted();
            return ++currentVerbPrepositionId <= maxCurrentVerbPrepositionId;
        }
        public bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            return StringHelper.Equals(this.verbPrepositions[currentVerbPrepositionId].Preposition.Value, answer);
        }
        #endregion

        #region Private methods
        private void ThrowIfItImpossibleToStart()
        {
            if (this.verbPrepositions is null || !this.verbPrepositions.Any())
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
