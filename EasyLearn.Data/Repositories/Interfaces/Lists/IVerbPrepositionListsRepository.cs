using EasyLearn.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IVerbPrepositionListsRepository
    {
        Task AddList(string name, string description, int userId);
        IEnumerable<VerbPrepositionList> GetUsersVerbPreposotionLists(int userId);
    }
}
