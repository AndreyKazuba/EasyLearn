using EasyLearn.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonDictionaryRepository
    {
        Task<CommonDictionary?> CreateCommonDictionary(string name, string description, int userId);
        CommonDictionary GetCommonDictionary(int dictionaryId);
        Task<CommonDictionary> GetCommonDictionaryAsync(int dictionaryId);
        IEnumerable<CommonDictionary> GetUsersCommonDictionaries(int userId);
        bool IsCommonDictionaryExist(int dictionaryId);
        public Task DeleteCommonDictionary(int dictionaryId);
    }
}
