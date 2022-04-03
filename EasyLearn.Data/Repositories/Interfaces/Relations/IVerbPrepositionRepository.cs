using EasyLearn.Data.Models;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionRepository
    {
        bool IsVerbPrepositionExist(int verbId, int prepositionId, int dictionaryId);
        bool IsVerbPrepositionExist(string verbValue, string prepositionValue, int dictionaryId);
        Task<VerbPreposition> CreateVerbPreposition(string verbValue, string prepositionValue, int dictionaryId, string translation, string? comment);
        Task DeleteAllDictionaryVerbPrepositions(int dictionaryId);
    }
}
