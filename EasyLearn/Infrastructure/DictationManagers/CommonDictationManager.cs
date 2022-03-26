using System;
using System.Collections.Generic;
using System.Linq;
using EasyLearn.Data.Models;

namespace EasyLearn.Infrastructure.DictationManagers
{
    public class CoommonDictationManager
    {
        private bool isStarted;
        private int currentRelationId;
        private int maxCurrentRelationId;
        private List<CommonRelation> allRelations;
        private List<CommonRelation> selectedRelations;
        private List<CommonRelation> synonymRelations;
        public CommonRelation CurrentRelation => selectedRelations[currentRelationId];
        public List<CommonRelation> AvailableRelations => synonymRelations;
        public bool CurrentRelationHasSynonyms => synonymRelations.Count > 1;

#pragma warning disable CS8618
        public CoommonDictationManager(List<CommonRelation> commonRelations, int dictationLength)
        {
            if (dictationLength <= 0 || dictationLength > commonRelations.Count)
                throw new ArgumentOutOfRangeException(nameof(dictationLength));
            this.allRelations = commonRelations;
            this.selectedRelations = new List<CommonRelation>(DictationManagerHelper.Shuffle(allRelations).Take(dictationLength));
            this.maxCurrentRelationId = dictationLength - 1;
        }
#pragma warning restore CS8618

        public CommonRelation Start()
        {
            if (this.selectedRelations is null || !this.selectedRelations.Any())
                throw new Exception("Нельзя начать диктант без слов");
            this.isStarted = true;
            SetSynonymRelations();
            return this.selectedRelations[currentRelationId];
        }
        public bool GoNext()
        {
            if (!this.isStarted)
                throw new Exception("Сначала нужно стартовать диктант");
            else if (++currentRelationId <= maxCurrentRelationId)
            {
                SetSynonymRelations();
                return true;
            }
            else
            {
                return false;
            }
        }
        private void SetSynonymRelations()
        {
            int currentRelationRussianUnitId = this.selectedRelations[currentRelationId].RussianUnit.Id;
            this.synonymRelations = this.allRelations
                .Where(relation => relation.RussianUnit.Id == currentRelationRussianUnitId)
                .ToList();
        }
    }
}
