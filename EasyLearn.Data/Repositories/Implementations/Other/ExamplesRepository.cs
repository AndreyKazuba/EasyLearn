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

        public ExamplesRepository(EasyLearnContext context)
        {
            this.context = context;
        }
        public bool IsExampleExist(int exampleId) => context.Examples.Any(example => example.Id == exampleId);
        public async Task<bool> IsExampleExistAsync(int exampleId) => await context.Examples.AnyAsync(example => example.Id == exampleId);
        public Example GetExample(int exampleId) => context.Examples.AsNoTracking().First(example => example.Id == exampleId);
        public async Task<Example> GetExampleAsync(int exampleId) => await context.Examples.AsNoTracking().FirstAsync(example => example.Id == exampleId);
        public Example? TryGetExample(int exampleId) => context.Examples.AsNoTracking().FirstOrDefault(example => example.Id == exampleId);
        public async Task<Example> CreateExample(string russianValue, string englishValue)
        {
            ThrowIfAddingAttemptIncorrect(russianValue, englishValue);
            Example newExample = new Example
            {
                RussianValue = russianValue,
                EnglishValue = englishValue,
                CreationDateUtc = DateTime.UtcNow,
            };
            context.Examples.Add(newExample);
            await context.SaveChangesAsync();
            return newExample;
        }

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(string russianValue, string englishValue)
        {
            // проверка values на валидность
        }
        #endregion
    }
}
