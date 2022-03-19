using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEasyLearnUserRerository
    {
        bool IsUserExist(string nickName);
        bool IsUserExist(int userId);
        Task<bool> IsUserExistAsync(string nickName);
        EasyLearnUser GetUserById(int userId);
        Task<EasyLearnUser> GetUserByIdAsync(int userId);
        Task<EasyLearnUser?> CreateUser(string name);
        IEnumerable<EasyLearnUser> GetAllUsers();
        EasyLearnUser GetCurrentUser();
        Task<EasyLearnUser> GetCurrentUserAsync();
        bool IsUserCurrent(int userId);
        Task SetUserAsCurrent(int userId);
        Task RemoveUser(int userId);
        Task EditUser(int userId, string newName);
    }
}
