using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionRepository
    {
        bool IsVerbPrepositionExist(int verbId, int prepositionId, int dictionaryId);
        bool IsVerbPrepositionExist(string verbValue, string prepositionValue, int dictionaryId);
        VerbPreposition GetVerbPreposition(int verbPrepositionId);
        Task<VerbPreposition> CreateVerbPreposition(
            string verbValue,
            string prepositionValue,
            int dictionaryId,
            string translation,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue);
        Task<VerbPreposition> UpdateVerbPreposition(
            int verbPrepositionId,
            string translation,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue);
        Task DeleteAllDictionaryVerbPrepositions(int dictionaryId);
    }
}
