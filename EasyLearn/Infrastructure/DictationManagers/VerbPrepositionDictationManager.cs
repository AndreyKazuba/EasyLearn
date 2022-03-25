using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class VerbPrepositionDictationManager
    {
        private bool isStarted;
        private int currentVerbPrepositionId;
        private int maxCurrentVerbPrepositionId;
        private List<VerbPreposition> verbPrepositions;
        public VerbPreposition CurrentVerbPreposition => verbPrepositions[currentVerbPrepositionId];
        public VerbPrepositionDictationManager(List<VerbPreposition> verbPrepositions)
        {
            this.verbPrepositions = verbPrepositions;
            this.maxCurrentVerbPrepositionId = verbPrepositions.Count - 1;
        }
        public VerbPreposition Start()
        {
            if (this.verbPrepositions is null || !this.verbPrepositions.Any())
                throw new Exception("Нельзя начать диктант без слов");
            this.isStarted = true;
            return this.verbPrepositions[currentVerbPrepositionId];
        }
        public bool GoNext()
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            else if (++currentVerbPrepositionId <= maxCurrentVerbPrepositionId)
                return true;
            else
                return false;
        }
        public bool IsAnswerCorrect(string answer)
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            return StringHelper.Equals(this.verbPrepositions[currentVerbPrepositionId].Preposition.Value, answer);
        }
    }
}
