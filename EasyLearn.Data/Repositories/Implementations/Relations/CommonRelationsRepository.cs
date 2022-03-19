using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class CommonRelationsRepository : ICommonRelationRepository
    {
        private readonly EasyLearnContext context;
        private readonly IEnglishUnitRepository englishUnitsRepository;
        private readonly IRussianUnitRepository russianUnitsRepository;
        private readonly ICommonDictionaryRepository commonWordListsRepository;

        public CommonRelationsRepository(EasyLearnContext context, 
            IRussianUnitRepository russianUnitsRepository,
            IEnglishUnitRepository englishUnitsRepository,
            ICommonDictionaryRepository commonWordListsRepository)
        {
            this.context = context;
            this.englishUnitsRepository = englishUnitsRepository;
            this.russianUnitsRepository = russianUnitsRepository;
            this.commonWordListsRepository = commonWordListsRepository;
        }

        public bool IsRelationExist(int rusUnitId, int engUnitId, int commonDictionaryId)
        {
            return context.CommonRelations.Any(relation => relation.RussianUnitId == rusUnitId && relation.EnglishUnitId == engUnitId && relation.CommonDictionaryId == commonDictionaryId);
        }

        public async Task<CommonRelation?> CreateCommonRelation(string rusUnitValue, UnitType rusUnitType, string engUnitValue, UnitType engUnitType, int commonDictionaryId, string? comment = null)
        {
            if (string.IsNullOrWhiteSpace(rusUnitValue) || string.IsNullOrWhiteSpace(engUnitValue))
            {
                return null;
            }

            if (!commonWordListsRepository.IsCommonDictionaryExist(commonDictionaryId))
            {
                return null;
            }

            EnglishUnit engUnit = await englishUnitsRepository.GetOrCreateUnit(engUnitValue, engUnitType);
            RussianUnit rusUnit = await russianUnitsRepository.GetOrCreateUnit(rusUnitValue, rusUnitType);

            if (IsRelationExist(rusUnit.Id, engUnit.Id, commonDictionaryId))
            {
                return null;
            }

            CommonRelation newRelation = new CommonRelation
            {
                EnglishUnitId = engUnit.Id,
                RussianUnitId = rusUnit.Id,
                CreationDateUtc = DateTime.UtcNow,
                CommonDictionaryId = commonDictionaryId,
                Comment = comment,
            };

            context.CommonRelations.Add(newRelation);
            await context.SaveChangesAsync();

            return newRelation;
        }

        public async Task DeleteAllDictionaryRelations(int dictionaryId)
        {
            IEnumerable<CommonRelation> commonRelations = context.CommonRelations.Where(relation => relation.CommonDictionaryId == dictionaryId);
            context.CommonRelations.RemoveRange(commonRelations);
            await context.SaveChangesAsync();
        }
    }
}
