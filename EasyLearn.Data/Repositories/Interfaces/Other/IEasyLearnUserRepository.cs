using System.Collections.Generic;
using System.Threading.Tasks;
using EasyLearn.Data.Models;

namespace EasyLearn.Data.Repositories.Interfaces
{
    public interface IEasyLearnUserRepository
    {
        bool IsUserCurrent(int userId);
        bool IsUserExist(int userId);
        bool IsUserExist(string userName);
        Task<bool> IsUserExistAsync(int userId);
        Task<bool> IsUserExistAsync(string userName);
        EasyLearnUser GetUser(int userId);
        Task<EasyLearnUser> GetUserAsync(int userId);
        EasyLearnUser? TryGetUser(int userId);
        EasyLearnUser? TryGetCurrentUser();
        Task<EasyLearnUser?> TryGetCurrentUserAsync();
        IEnumerable<EasyLearnUser> GetAllUsers();
        Task<EasyLearnUser> CreateUser(string userName);
        Task DeleteUser(int userId);
        Task EditUser(int userId, string userName);
        Task SetUserAsCurrent(int userId);
    }
}
