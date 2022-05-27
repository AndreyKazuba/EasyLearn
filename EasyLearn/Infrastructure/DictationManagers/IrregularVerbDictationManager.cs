using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class IrregularVerbDictationManager
    {
        #region Private fields
        private bool isStarted;
        private bool currentAnswerIsNew;
        private int answersCounter;
        private int wrongAnswersCounter;
        private int currentIrregularVerbId;
        private int maxCurrentIrregularVerbId;
        private List<IrregularVerb> irregularVerbs;
        #endregion

        #region Public props
        public IrregularVerb CurrentIrregularVerb => irregularVerbs[currentIrregularVerbId];
        public string CurrentV1Value => CurrentIrregularVerb.FirstForm.Value;
        public string CurrentV2Value => CurrentIrregularVerb.SecondForm.Value;
        public string CurrentV3Value => CurrentIrregularVerb.ThirdForm.Value;
        public int TotalIrregularVerbsCount => irregularVerbs.Count;
        public int AnswersCount => answersCounter;
        public int WrongAnswersCount => wrongAnswersCounter;
        #endregion

        public IrregularVerbDictationManager(List<IrregularVerb> irregularVerbs)
        {
            this.irregularVerbs = irregularVerbs;
            this.maxCurrentIrregularVerbId = irregularVerbs.Count - 1;
        }

        #region Public dictation process methods
        public IrregularVerb Start()
        {
            ThrowIfItImpossibleToStart();
            isStarted = true;
            currentAnswerIsNew = true;
            answersCounter = 0;
            wrongAnswersCounter = 0;
            return irregularVerbs[currentIrregularVerbId];
        }
        public bool GoNext()
        {
            ThrowIfDictationIsNotStarted();
            currentAnswerIsNew = true;
            return ++currentIrregularVerbId <= maxCurrentIrregularVerbId;
        }
        public bool IsV1AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].FirstForm.Value, answer);
            if (currentAnswerIsNew)
            {
                answersCounter++;
                currentAnswerIsNew = false;
                if (!answerIsCorrect)
                    wrongAnswersCounter++;
            }
            return answerIsCorrect;
        }
        public bool IsV2AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].SecondForm.Value, answer);
            if (currentAnswerIsNew)
            {
                answersCounter++;
                currentAnswerIsNew = false;
                if (!answerIsCorrect)
                    wrongAnswersCounter++;
            }
            return answerIsCorrect;
        }
        public bool IsV3AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].ThirdForm.Value, answer);
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

        #region Private throwers
        private void ThrowIfItImpossibleToStart()
        {
            if (irregularVerbs is null || !irregularVerbs.Any())
                throw new Exception(ExceptionMessagesHelper.CannotStartDictationWithoutWords);
        }
        private void ThrowIfDictationIsNotStarted()
        {
            if (!isStarted)
                throw new Exception(ExceptionMessagesHelper.NeedsToStarDictationFirst);
        }
        #endregion
    }
}
