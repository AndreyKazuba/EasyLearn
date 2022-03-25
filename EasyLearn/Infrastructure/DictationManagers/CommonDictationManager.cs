using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class CommonDictationManager
    {
        private bool isStarted;
        private int currentRelationId;
        private int maxCurrentRelationId;
        private List<CommonRelation> commonRelations;
        public CommonRelation CurrentCommonRelation => commonRelations[currentRelationId];
        public CommonDictationManager(List<CommonRelation> commonRelations)
        {
            this.commonRelations = commonRelations;
            this.maxCurrentRelationId = commonRelations.Count - 1;
        }
        public CommonRelation Start()
        {
            if (this.commonRelations is null || !this.commonRelations.Any())
                throw new Exception("Нельзя начать диктант без слов");
            this.isStarted = true;
            return this.commonRelations[currentRelationId];
        }
        public bool GoNext()
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            else if (++currentRelationId <= maxCurrentRelationId)
                return true;
            else
                return false;
        }
        public bool IsAnswerCorrect(string answer)
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            return StringHelper.Equals(this.commonRelations[currentRelationId].EnglishUnit.Value, answer);
        }
    }
}
