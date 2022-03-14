using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEasyLearnUsersRerository
    {
        bool IsUserExist(string nickName);
        bool IsUserExist(int userId);
        Task<bool> IsUserExistAsync(string nickName);
        EasyLearnUser GetUserById(int userId);
        Task<EasyLearnUser> GetUserByIdAsync(int userId);
        Task<bool> AddUser(string nickName);
        IEnumerable<EasyLearnUser> GetAllUsers();
        EasyLearnUser GetCurrentUser();
        Task<EasyLearnUser> GetCurrentUserAsync();
        bool IsUserCurrent(int userId);
        Task SetCurrentUser(int userId);
        Task RemoveUser(int userId);
    }
}
