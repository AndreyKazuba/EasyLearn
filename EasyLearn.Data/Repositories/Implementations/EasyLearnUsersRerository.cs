using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class EasyLearnUsersRerository : IEasyLearnUsersRerository
    {
        private readonly EasyLearnDbContext context;

        public EasyLearnUsersRerository(EasyLearnDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<EasyLearnUser> GetAllUsers()
        {
            return context.Users.AsNoTracking().AsEnumerable();
        }

        public bool IsUserExist(string nickName)
        {
            return context.Users.Any(user => user.NickName == nickName);
        }

        public async Task<bool> IsUserExistAsync(string nickName)
        {
            return await context.Users.AnyAsync(user => user.NickName == nickName);
        }

        public EasyLearnUser GetUserById(int userId)
        {
            return context.Users.FirstOrDefault(user => user.Id == userId);
        }

        public async Task<EasyLearnUser> GetUserByIdAsync(int userId)
        {
            return await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
        }

        public EasyLearnUser GetCurrentUser()
        {
            return context.Users.First(user => user.IsCurrent);
        }

        public async Task<EasyLearnUser> GetCurrentUserAsync()
        {
            return await context.Users.FirstAsync(user => user.IsCurrent);
        }

        public bool IsUserCurrent(int userId)
        {
            return context.Users.First(user => user.Id == userId).IsCurrent;
        }

        public async Task SetCurrentUser(int userId)
        {
            await ResetCurrentUser();
            EasyLearnUser newCurrentUser = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);

            newCurrentUser.IsCurrent = true;
            await context.SaveChangesAsync();
        }

        public async Task RemoveUser(int userId)
        {
            EasyLearnUser user = await context.Users.FirstOrDefaultAsync(user => user.Id == userId);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> AddUser(string nickName)
        {
            if (string.IsNullOrEmpty(nickName))
            {
                return false;
            }

            if (IsUserExist(nickName))
            {
                return false;
            }

            EasyLearnUser newUser = new EasyLearnUser
            {
                NickName = nickName,
                IsCurrent = true,
            };

            await ResetCurrentUser();
            context.Users.Add(newUser);
            await context.SaveChangesAsync();

            return true;
        }

        private async Task ResetCurrentUser()
        {
            EasyLearnUser? currentUser = await context.Users.FirstOrDefaultAsync(user => user.IsCurrent);

            if (currentUser is not null)
            {
                currentUser.IsCurrent = false;
            }
        }

        public bool IsUserExist(int userId)
        {
            return context.Users.Any(user => user.Id == userId);
        }
    }
}
