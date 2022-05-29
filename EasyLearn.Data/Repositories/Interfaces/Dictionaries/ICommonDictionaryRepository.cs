using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonDictionaryRepository
    {
        bool IsCommonDictionaryExist(int dictionaryId);
        CommonDictionary GetCommonDictionary(int dictionaryId);
        Task<CommonDictionary> GetCommonDictionaryAsync(int dictionaryId);
        IEnumerable<CommonDictionary> GetUsersCommonDictionaries(int userId);
        Task<CommonDictionary> CreateCommonDictionary(string name, int userId);
        Task DeleteCommonDictionary(int dictionaryId);
        Task EditCommonDictionary(int dictionaryId, string name);
    }
}
