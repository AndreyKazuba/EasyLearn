using EasyLearn.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionDictionaryRepository
    {
        Task<VerbPrepositionDictionnary?> CreateVerbPrepositionDictionary(string name, string description, int userId);
        IEnumerable<VerbPrepositionDictionnary> GetUsersVerbPreposotionDictionaries(int userId);
        VerbPrepositionDictionnary? GetVerbPrepositionDictionary(int dictionaryId);
        Task<VerbPrepositionDictionnary?> GetVerbPrepositionDictionaryAsync(int dictionaryId);
        Task DeleteVerbPrepositionDictionary(int dictionaryId);
        bool IsVerbPrepositionDictionaryExist(int dictionaryId);
    }
}
