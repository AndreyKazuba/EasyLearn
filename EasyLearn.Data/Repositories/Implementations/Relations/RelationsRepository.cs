using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class RelationsRepository : IRelationsRepository
    {
        private readonly EasyLearnDbContext context;
        private readonly IEnglishUnitsRepository englishUnitsRepository;
        private readonly IRussianUnitsRepository russianUnitsRepository;
        private readonly ICommonWordListsRepository commonWordListsRepository;

        public RelationsRepository(EasyLearnDbContext context, 
            IRussianUnitsRepository russianUnitsRepository,
            IEnglishUnitsRepository englishUnitsRepository,
            ICommonWordListsRepository commonWordListsRepository)
        {
            this.context = context;
            this.englishUnitsRepository = englishUnitsRepository;
            this.russianUnitsRepository = russianUnitsRepository;
            this.commonWordListsRepository = commonWordListsRepository;
        }

        public bool IsRelationExist(int rusUnitId, int engUnitId)
        {
            return context.CommonRelations.Any(relation => relation.RussianWordId == rusUnitId && relation.EnglishWordId == engUnitId);
        }

        public async Task<CommonRelation?> CreateRelation(string rusUnitValue, UnitType rusUnitType, string engUnitValue, UnitType engUnitType, int wordListId, string? comment = null)
        {
            if (string.IsNullOrWhiteSpace(rusUnitValue) || string.IsNullOrEmpty(engUnitValue))
            {
                return null;
            }

            if (!commonWordListsRepository.IsWordListExist(wordListId))
            {
                return null;
            }

            EnglishUnit engUnit = await englishUnitsRepository.GetOrCreateUnit(engUnitValue, engUnitType);
            RussianUnit rusUnit = await russianUnitsRepository.GetOrCreateUnit(rusUnitValue, rusUnitType);

            if (IsRelationExist(rusUnit.Id, engUnit.Id))
            {
                return null;
            }

            CommonRelation newRelation = new CommonRelation
            {
                EnglishWordId = engUnit.Id,
                RussianWordId = rusUnit.Id,
                CreationDateUtc = DateTime.UtcNow,
                WordListId = wordListId,
                Comment = comment,
            };

            context.CommonRelations.Add(newRelation);
            await context.SaveChangesAsync();

            return newRelation;
        }
    }
}
