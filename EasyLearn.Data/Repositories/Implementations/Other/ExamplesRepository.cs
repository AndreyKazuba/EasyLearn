using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Exceptions;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class ExamplesRepository : IExamplesRepository
    {
        private readonly EasyLearnContext context;
        private readonly IRussianUnitRepository russianUnitRepository;
        private readonly IEnglishUnitRepository englishUnitRepository;

        public ExamplesRepository(EasyLearnContext context, IRussianUnitRepository russianUnitRepository, IEnglishUnitRepository englishUnitRepository)
        {
            this.context = context;
            this.russianUnitRepository = russianUnitRepository;
            this.englishUnitRepository = englishUnitRepository;
        }
        public bool IsExampleExist(int exampleId) => context.Examples.Any(example => example.Id == exampleId);
        public bool IsExampleExist(int russianTranslationId, int englishTranslationId) => context.Examples.Any(example => example.RussianTranslationId == russianTranslationId && example.EnglishTranslationId == englishTranslationId);
        public async Task<bool> IsExampleExistAsync(int exampleId) => await context.Examples.AnyAsync(example => example.Id == exampleId);
        public async Task<bool> IsExampleExistAsync(int russianTranslationId, int englishTranslationId) => await context.Examples.AnyAsync(example => example.RussianTranslationId == russianTranslationId && example.EnglishTranslationId == englishTranslationId);
        public Example GetExample(int exampleId) => context.Examples.AsNoTracking().First(example => example.Id == exampleId);
        public Example GetExample(int russianTranslationId, int englishTranslationId) => context.Examples.AsNoTracking().First(example => example.RussianTranslationId == russianTranslationId && example.EnglishTranslationId == englishTranslationId);
        public async Task<Example> GetExampleAsync(int exampleId) => await context.Examples.AsNoTracking().FirstAsync(example => example.Id == exampleId);
        public async Task<Example> GetExampleAsync(int russianTranslationId, int englishTranslationId) => await context.Examples.AsNoTracking().FirstAsync(example => example.RussianTranslationId == russianTranslationId && example.EnglishTranslationId == englishTranslationId);
        public Example? TryGetExample(int exampleId) => context.Examples.AsNoTracking().FirstOrDefault(example => example.Id == exampleId);
        public Example? TryGetExample(int russianTranslationId, int englishTranslationId) => context.Examples.AsNoTracking().FirstOrDefault(example => example.RussianTranslationId == russianTranslationId && example.EnglishTranslationId == englishTranslationId);
        public async Task<Example> CreateExample(string russianTranslationValue, UnitType russianTranslationType, string englishTranslationValue, UnitType englishTranslationType)
        {
            RussianUnit russianTranslation = await russianUnitRepository.GetOrCreateUnit(russianTranslationValue, russianTranslationType);
            EnglishUnit englishTranslation = await englishUnitRepository.GetOrCreateUnit(englishTranslationValue, englishTranslationType);
            ThrowIfAddingAttemptIncorrect(russianTranslation.Id, englishTranslation.Id);
            Example newExample = new Example
            {
                RussianTranslationId = russianTranslation.Id,
                EnglishTranslationId = englishTranslation.Id,
                CreationDateUtc = DateTime.UtcNow,
            };
            context.Examples.Add(newExample);
            await context.SaveChangesAsync();
            newExample.RussianTranslation = russianTranslation;
            newExample.EnglishTranslation = englishTranslation;
            return newExample;
        }
        public async Task<Example> GetOrCreateExample(string russianTranslationValue, UnitType russianTranslationType, string englishTranslationValue, UnitType englishTranslationType)
        {
            RussianUnit russianTranslation = await russianUnitRepository.GetOrCreateUnit(russianTranslationValue, russianTranslationType);
            EnglishUnit englishTranslation = await englishUnitRepository.GetOrCreateUnit(englishTranslationValue, englishTranslationType);
            Example? example = TryGetExample(russianTranslation.Id, englishTranslation.Id);
            return example is not null ? example : await CreateExample(russianTranslationValue, russianTranslationType, englishTranslationValue, englishTranslationType);
        }
        #region Private members
        private void ThrowIfAddingAttemptIncorrect(int russianTranslationId, int englishTranslationId)
        {
            if (IsExampleExist(russianTranslationId, englishTranslationId))
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.AttemptToAddExistingEntity(nameof(Example), nameof(Example.RussianTranslationId), russianTranslationId.ToString(), nameof(Example.EnglishTranslationId), englishTranslationId.ToString()));
        }
        #endregion
    }
}
