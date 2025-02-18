﻿using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.DTO;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.Helpers;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class VerbPrepositionDictationManager
    {
        #region Private fields
        protected bool currentAnswerIsNew;
        private bool isStarted;
        private int answersCounter;
        private int wrongAnswersCounter;
        private int currentVerbPrepositionId;
        private int maxCurrentVerbPrepositionId;
        private List<Answer> answers = new List<Answer>();
        private List<VerbPreposition> verbPrepositions;
        #endregion

        #region Public props
        public VerbPreposition CurrentVerbPreposition => verbPrepositions[currentVerbPrepositionId];
        public string CurrentPrepositionValue => CurrentVerbPreposition.Preposition.Value;
        public string CurrentVerbValue => CurrentVerbPreposition.Verb.Value;
        public int TotalVerbPrepositionsCount => verbPrepositions.Count;
        public int AnswersCount => answersCounter;
        public int WrongAnswersCount => wrongAnswersCounter;
        #endregion

        public VerbPrepositionDictationManager(List<VerbPreposition> verbPrepositions, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > verbPrepositions.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            this.verbPrepositions = SelectVerbPrepositions(verbPrepositions, dictationLength);
            maxCurrentVerbPrepositionId = dictationLength - 1;
        }

        #region Public dictation process methods
        public VerbPreposition Start()
        {
            ThrowIfItImpossibleToStart();
            isStarted = true;
            currentAnswerIsNew = true;
            answersCounter = 0;
            wrongAnswersCounter = 0;
            return verbPrepositions[currentVerbPrepositionId];
        }
        public bool GoNext()
        {
            ThrowIfDictationIsNotStarted();
            currentAnswerIsNew = true;
            return ++currentVerbPrepositionId <= maxCurrentVerbPrepositionId;
        }
        public bool IsAnswerCorrect(string answer)
        {
            ThrowIfDictationIsNotStarted();
            bool answerIsCorrect = StringHelper.Equals(verbPrepositions[currentVerbPrepositionId].Preposition.Value, answer);
            if (currentAnswerIsNew)
            {
                answersCounter++;
                answers.Add(new Answer { RelationId = verbPrepositions[currentVerbPrepositionId].Id, Variation = answerIsCorrect ? AnswerVariation.FirstTry : AnswerVariation.SecondTry });
                currentAnswerIsNew = false;
                if (!answerIsCorrect)
                    wrongAnswersCounter++;
            }
            else if (!answerIsCorrect)
            {
                Answer lastAnswer = answers.Last();
                lastAnswer.Variation = lastAnswer.Variation == AnswerVariation.SecondTry 
                    ? AnswerVariation.ThirdTry 
                    : AnswerVariation.FourthPlusTry;
            }
            return answerIsCorrect;
        }
        public void SaveDictationResults() => App.GetService<IVerbPrepositionRepository>().SaveDictationResults(answers);
        #endregion

        #region Private methods
        private List<VerbPreposition> SelectVerbPrepositions(List<VerbPreposition> verbPrepositions, int dictationLength)
        {
            int unstudiedVerbPrepositionsCount = verbPrepositions.Count(verbPreposition => !verbPreposition.Studied);
            if (dictationLength <= unstudiedVerbPrepositionsCount)
            {
                return new List<VerbPreposition>(verbPrepositions.OrderBy(verbPreposition => verbPreposition.Priority).Take(dictationLength).Shuffle());
            }
            else
            {
                int additionanVerbPrepositionsCount = dictationLength - unstudiedVerbPrepositionsCount;
                List<VerbPreposition> unstudiedVerbPrepositions = new List<VerbPreposition>(verbPrepositions.Where(verbPreposition => !verbPreposition.Studied));
                List<VerbPreposition> additionalVerbPrepositions = new List<VerbPreposition>(verbPrepositions.Where(verbPreposition => verbPreposition.Studied).Shuffle().Take(additionanVerbPrepositionsCount));
                return unstudiedVerbPrepositions.Union(additionalVerbPrepositions).Shuffle().ToList();
            }
        }
        #endregion

        #region Private throwers
        private void ThrowIfItImpossibleToStart()
        {
            if (verbPrepositions is null || !verbPrepositions.Any())
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
