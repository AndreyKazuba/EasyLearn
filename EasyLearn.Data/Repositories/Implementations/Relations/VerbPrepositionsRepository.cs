using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionsRepository : IVerbPrepositionRepository
    {
        private readonly IEnglishUnitsRepository englishUnitsRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        private readonly EasyLearnContext context;

        public VerbPrepositionsRepository(EasyLearnContext context, IEnglishUnitsRepository englishUnitsRepository, IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository)
        {
            this.context = context;
            this.englishUnitsRepository = englishUnitsRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
        }

        public async Task<VerbPreposition?> CreateVerbPreposition(string verbValue, string prepositionValue, int verbPrepositionDictionaryId, string? comment)
        {
            if (string.IsNullOrWhiteSpace(verbValue) || string.IsNullOrWhiteSpace(prepositionValue))
            {
                return null;
            }

            if (!verbPrepositionDictionaryRepository.IsVerbPrepositionDictionaryExist(verbPrepositionDictionaryId))
            {
                return null;
            }

            EnglishUnit verb = await englishUnitsRepository.GetOrCreateUnit(verbValue, UnitType.Verb);
            EnglishUnit preposition = await englishUnitsRepository.GetOrCreateUnit(prepositionValue, UnitType.Preposition);

            VerbPreposition newVerbPreposition = new VerbPreposition
            {
                VerbId = verb.Id,
                PrepositionId = preposition.Id,
                Comment = comment,
                CreationDateUtc = DateTime.UtcNow,
                VerbPrepositionDictionaryId = verbPrepositionDictionaryId,
            };

            context.VerbPrepositions.Add(newVerbPreposition);
            await context.SaveChangesAsync();
            return newVerbPreposition;
        }
    }
}
