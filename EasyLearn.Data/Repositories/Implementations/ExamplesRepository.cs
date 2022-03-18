using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Enums;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class ExamplesRepository : IExamplesRepository
    {
        private readonly EasyLearnContext context;
        private readonly IRussianUnitsRepository russianUnitsRepository;
        private readonly IEnglishUnitsRepository englishUnitsRepository;

        public ExamplesRepository(EasyLearnContext context, IRussianUnitsRepository russianUnitsRepository, IEnglishUnitsRepository englishUnitsRepository)
        {
            this.context = context;
            this.russianUnitsRepository = russianUnitsRepository;
            this.englishUnitsRepository = englishUnitsRepository;
        }

        public bool IsExampleExist(int exampleId)
        {
            return context.Examples.Any(example => example.Id == exampleId);
        }

        public async Task<bool> IsExampleExistAsync(int exampleId)
        {
            return await context.Examples.AnyAsync(example => example.Id == exampleId);
        }

        public bool IsExampleExist(string rusTranslation, string engTranslation)
        {
            if (string.IsNullOrEmpty(rusTranslation) || string.IsNullOrEmpty(engTranslation))
            {
                return false;
            }

            RussianUnit russianTranslation = russianUnitsRepository.GetUnitByValueAndType(rusTranslation, UnitType.Sentence);
            EnglishUnit englishTranslation = englishUnitsRepository.GetUnitByValueAndType(engTranslation, UnitType.Sentence);

            if (russianTranslation == null || englishTranslation == null)
            {
                return false;
            }

            return context.Examples.Any(example => example.RussianTranslationId == russianTranslation.Id && example.EnglishTranslationId == englishTranslation.Id);
        }

        public async Task<bool> IsExampleExistAsync(string rusTranslation, string engTranslation)
        {
            if (string.IsNullOrEmpty(rusTranslation) || string.IsNullOrEmpty(engTranslation))
            {
                return false;
            }

            RussianUnit russianTranslation = await russianUnitsRepository.GetUnitByValueAndTypeAsync(rusTranslation, UnitType.Sentence);
            EnglishUnit englishTranslation = await englishUnitsRepository.GetUnitByValueAndTypeAsync(engTranslation, UnitType.Sentence);

            if (russianTranslation == null || englishTranslation == null)
            {
                return false;
            }

            return await context.Examples.AnyAsync(example => example.RussianTranslationId == russianTranslation.Id && example.EnglishTranslationId == englishTranslation.Id);
        }

        public Example GetExampleById(int exampleId)
        {
            return context.Examples.FirstOrDefault(example => example.Id == exampleId);
        }

        public async Task<Example> GetExampleByIdAsync(int exampleId)
        {
            return await context.Examples.FirstOrDefaultAsync(example => example.Id == exampleId);
        }

        //public async Task<bool> AddExample(RussianUnit rusTranslation, EnglishUnit engTranslation)
        //{
        //    if (rusTranslation == null || engTranslation == null)
        //    {
        //        return false;
        //    }

        //    if (!await russianUnitsRepository.IsUnitExistAsync(rusTranslation.Id) && !await englishUnitsRepository.IsUnitExistAsync(engTranslation.Id))
        //    {
        //        return false;
        //    }

        //    if (await IsExampleExistAsync(rusTranslation.Value, engTranslation.Value))
        //    {
        //        return false;
        //    }
        //}
    }
}
