using EasyLearn.Data.DTO;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class IrregularVerbDictationManager
    {
        #region Private fields
        private bool isStarted;
        private bool currentV1AnswerIsNew;
        private bool currentV2AnswerIsNew;
        private bool currentV3AnswerIsNew;
        private bool v1WasIncorrect;
        private bool v2WasIncorrect;
        private bool v3WasIncorrect;
        private bool wrongAnswersCounterWasIncresed;
        private int answersCounter;
        private int wrongAnswersCounter;
        private int currentIrregularVerbId;
        private int maxCurrentIrregularVerbId;
        private List<Answer> answers = new List<Answer>();
        private List<IrregularVerb> irregularVerbs;
        #endregion

        #region Private helper props
        private bool CurrentAnswerIsIncorrect => v1WasIncorrect || v2WasIncorrect || v3WasIncorrect;
        private int IncorrectAnswers
        {
            get
            {
                if ((v1WasIncorrect && !v2WasIncorrect && !v3WasIncorrect)
                || (!v1WasIncorrect && v2WasIncorrect && !v3WasIncorrect)
                || (!v1WasIncorrect && !v2WasIncorrect && v3WasIncorrect))
                    return 1;
                else if (v1WasIncorrect && v2WasIncorrect && v3WasIncorrect)
                    return 3;
                else return 2;
            }
        }
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

        public IrregularVerbDictationManager(List<IrregularVerb> irregularVerbs, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > irregularVerbs.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            this.irregularVerbs = new List<IrregularVerb>(irregularVerbs.OrderBy(irregularVerb => irregularVerb.Priority).Take(dictationLength).Shuffle());
            maxCurrentIrregularVerbId = dictationLength - 1;
        }

        #region Public dictation process methods
        public IrregularVerb Start()
        {
            ThrowIfItImpossibleToStart();
            isStarted = true;
            wrongAnswersCounterWasIncresed = false;
            currentV1AnswerIsNew = true;
            currentV2AnswerIsNew = true;
            currentV3AnswerIsNew = true;
            v1WasIncorrect = false;
            v2WasIncorrect = false;
            v3WasIncorrect = false;
            answersCounter = 0;
            wrongAnswersCounter = 0;
            return irregularVerbs[currentIrregularVerbId];
        }
        public bool GoNext()
        {
            ThrowIfDictationIsNotStarted();
            currentV1AnswerIsNew = true;
            currentV2AnswerIsNew = true;
            currentV3AnswerIsNew = true;
            wrongAnswersCounterWasIncresed = false;
            v1WasIncorrect = false;
            v2WasIncorrect = false;
            v3WasIncorrect = false;
            return ++currentIrregularVerbId <= maxCurrentIrregularVerbId;
        }
        public bool IsV1AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].FirstForm.Value, answer);
            if (currentV1AnswerIsNew)
            {
                answersCounter++;
                currentV1AnswerIsNew = false;
                if (!answerIsCorrect)
                    v1WasIncorrect = true;
                CheckWrongAnswersCounter();
            }
            return answerIsCorrect;
        }
        public bool IsV2AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].SecondForm.Value, answer);
            if (currentV2AnswerIsNew)
            {
                currentV2AnswerIsNew = false;
                if (!answerIsCorrect)
                    v2WasIncorrect = true;
                CheckWrongAnswersCounter();
            }
            return answerIsCorrect;
        }
        public bool IsV3AnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(irregularVerbs[currentIrregularVerbId].ThirdForm.Value, answer);
            if (currentV3AnswerIsNew)
            {
                currentV3AnswerIsNew = false;
                if (!answerIsCorrect)
                    v3WasIncorrect = true;
                CheckWrongAnswersCounter();
                answers.Add(new Answer
                {
                    RelationId = irregularVerbs[currentIrregularVerbId].Id,
                    Variation = !CurrentAnswerIsIncorrect
                    ? AnswerVariation.FirstTry
                    : IncorrectAnswers == 1
                        ? AnswerVariation.SecondTry
                        : IncorrectAnswers == 2
                            ? AnswerVariation.ThirdTry
                            : AnswerVariation.FourthPlusTry
                });
            }
            return answerIsCorrect;
        }
        public void SaveDictationResults() => App.GetService<IIrregularVerbRepository>().SaveDictationResults(answers);
        #endregion

        #region Private helpers
        private void CheckWrongAnswersCounter()
        {
            if (wrongAnswersCounterWasIncresed)
                return;
            if (CurrentAnswerIsIncorrect)
                wrongAnswersCounter++;
            wrongAnswersCounterWasIncresed = true;
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
