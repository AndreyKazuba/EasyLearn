using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Infrastructure.Dictation
{
    public class CommonDictationManager
    {
        private int currentRelationId;
        private int maxCurrentRelationId;
        private List<CommonRelation> relations;

        public CommonRelation CurrentRelation { get => relations[currentRelationId]; }

        public CommonDictationManager(List<CommonRelation> relations)
        {
            this.relations = relations;
            this.currentRelationId = 0;
            this.maxCurrentRelationId = relations.Count - 1;
        }

        public CommonRelation Start()
        {
            if (this.relations is null || !this.relations.Any())
            {
                throw new Exception("Something went wrong");
            }

            return this.relations[currentRelationId];
        }

        public bool GoNext()
        {
            if (++currentRelationId <= maxCurrentRelationId)
                return true;
            else 
                return false;
        }

        public bool IsAnswerCorrect(string answer)
        {
            return StringHelper.Equals(this.relations[currentRelationId].EnglishUnit.Value, answer);
        }
    }
}
