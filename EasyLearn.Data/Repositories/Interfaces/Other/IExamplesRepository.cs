using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IExamplesRepository
    {
        bool IsExampleExist(int exampleId);
        bool IsExampleExist(int russianTranslationId, int englishTranslationId);
        Task<bool> IsExampleExistAsync(int exampleId);
        Task<bool> IsExampleExistAsync(int russianTranslationId, int englishTranslationId);
        Example GetExample(int exampleId);
        Example GetExample(int russianTranslationId, int englishTranslationId);
        Task<Example> GetExampleAsync(int exampleId);
        Task<Example> GetExampleAsync(int russianTranslationId, int englishTranslationId);
        Example? TryGetExample(int exampleId);
        Example? TryGetExample(int russianTranslationId, int englishTranslationId);
        Task<Example> CreateExample(string russianTranslationValue, UnitType russianTranslationType, string englishTranslationValue, UnitType englishTranslationType);
        Task<Example> GetOrCreateExample(string russianTranslationValue, UnitType russianTranslationType, string englishTranslationValue, UnitType englishTranslationType);

    }
}
