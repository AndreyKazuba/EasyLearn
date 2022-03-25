using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class IrregularVerbDictationManager
    {
        private bool isStarted;
        private int currentIrregularVerbId;
        private int maxCurrentIrregularVerbId;
        private List<IrregularVerb> irregularVerbs;
        public IrregularVerb CurrentIrregularVerb => irregularVerbs[currentIrregularVerbId];
        public IrregularVerbDictationManager(List<IrregularVerb> irregularVerbs)
        {
            this.irregularVerbs = irregularVerbs;
            this.maxCurrentIrregularVerbId = irregularVerbs.Count - 1;
        }
        public IrregularVerb Start()
        {
            if (this.irregularVerbs is null || !this.irregularVerbs.Any())
                throw new Exception("Нельзя начать диктант без слов");
            this.isStarted = true;
            return this.irregularVerbs[currentIrregularVerbId];
        }
        public bool GoNext()
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            else if (++currentIrregularVerbId <= maxCurrentIrregularVerbId)
                return true;
            else
                return false;
        }
        public bool IsFirstFormAnswerCorrect(string answer)
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            return StringHelper.Equals(this.irregularVerbs[currentIrregularVerbId].FirstForm.Value, answer);
        }
        public bool IsSecondFormAnswerCorrect(string answer)
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            return StringHelper.Equals(this.irregularVerbs[currentIrregularVerbId].SecondForm.Value, answer);
        }
        public bool IsThirdFormAnswerCorrect(string answer)
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            return StringHelper.Equals(this.irregularVerbs[currentIrregularVerbId].ThirdForm.Value, answer);
        }
    }
}
