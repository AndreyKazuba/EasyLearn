using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonWordListsRepository
    {
        Task AddList(string name, string description, int userId, CommonWordListType type);
        IEnumerable<CommonWordList> GetUsersCommonLists(int userId);
        CommonWordList GetCommonWordList(int listId);
        Task<CommonWordList> GetCommonWordListAsync(int listId);
        bool IsWordListExist(int userListId);
    }
}
