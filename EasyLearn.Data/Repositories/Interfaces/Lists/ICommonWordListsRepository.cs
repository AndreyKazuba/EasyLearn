using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface ICommonWordListsRepository
    {
        public Task AddList(string name, string description, int userId, CommonWordListType type);
        public IEnumerable<CommonWordList> GetUsersCommonLists(int userId);
        public CommonWordList GetCommonWordList(int listId);
    }
}
