using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionDictionaryRepository
    {
        bool IsVerbPrepositionDictionaryExist(int dictionaryId);
        VerbPrepositionDictionnary GetVerbPrepositionDictionary(int dictionaryId);
        Task<VerbPrepositionDictionnary> GetVerbPrepositionDictionaryAsync(int dictionaryId);
        IEnumerable<VerbPrepositionDictionnary> GetUsersVerbPreposotionDictionaries(int userId);
        Task<VerbPrepositionDictionnary> CreateVerbPrepositionDictionary(string name, int userId);
        Task DeleteVerbPrepositionDictionary(int dictionaryId);
        Task EditVerbPrepositionDictionary(int dictionaryId, string name);
    }
}
