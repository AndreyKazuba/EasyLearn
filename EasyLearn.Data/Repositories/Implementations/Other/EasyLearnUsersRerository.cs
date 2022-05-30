using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Constants;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class EasyLearnUsersRerository : Repository, IEasyLearnUserRepository
    {
        public EasyLearnUsersRerository(EasyLearnContext context) : base(context) { }

        #region Public members
        public bool IsAtLeastOneUserExist() => context.Users.Any();
        public bool IsUserCurrent(int userId) => context.Users.First(user => user.Id == userId).IsCurrent;
        public bool IsUserExist(int userId) => context.Users.Any(user => user.Id == userId);
        public bool IsUserExist(string userName) => context.Users.Any(user => user.Name == userName);
        //public async Task<bool> IsUserExistAsync(int userId) => await context.Users.AnyAsync(user => user.Id == userId);
        //public async Task<bool> IsUserExistAsync(string userName) => await context.Users.AnyAsync(user => user.Name == userName);
        public EasyLearnUser GetUser(int userId) => context.Users.AsNoTracking().First(user => user.Id == userId);
        //public async Task<EasyLearnUser> GetUserAsync(int userId) => await context.Users.AsNoTracking().FirstAsync(user => user.Id == userId);
        public EasyLearnUser? TryGetUser(int userId) => context.Users.AsNoTracking().FirstOrDefault(user => user.Id == userId);
        public EasyLearnUser? TryGetCurrentUser() => context.Users.AsNoTracking().FirstOrDefault(user => user.IsCurrent);
        //public async Task<EasyLearnUser?> TryGetCurrentUserAsync() => await context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.IsCurrent);
        public IEnumerable<EasyLearnUser> GetAllUsers() => context.Users
            .Include(user => user.CommonDictionaries).ThenInclude(commonDictionary => commonDictionary.Relations)
            .Include(user => user.VerbPrepositionDictionaries).ThenInclude(verbPrepositionDictionary => verbPrepositionDictionary.VerbPrepositions)
            .AsNoTracking()
            .AsEnumerable();
        public async Task<EasyLearnUser> CreateUser(string userName)
        {
            ThrowIfUserNameIsInvalid(userName);
            EasyLearnUser newUser = new EasyLearnUser
            {
                Name = StringHelper.Prepare(userName),
            };
            context.Users.Add(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }
        public async Task DeleteUser(int userId)
        {
            EasyLearnUser user = await context.Users.FirstAsync(user => user.Id == userId);
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
        public async Task EditUser(int userId, string userName)
        {
            ThrowIfUserNameIsInvalid(userName);
            EasyLearnUser user = context.Users.First(user => user.Id == userId);
            user.Name = StringHelper.Prepare(userName);
            await context.SaveChangesAsync();
        }
        public async Task SetUserAsCurrent(int userId)
        {
            await TryResetCurrentUser();
            EasyLearnUser newCurrentUser = await context.Users.FirstAsync(user => user.Id == userId);
            newCurrentUser.IsCurrent = true;
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private async Task TryResetCurrentUser()
        {
            EasyLearnUser? currentUser = await context.Users.FirstOrDefaultAsync(user => user.IsCurrent);
            if (currentUser is not null)
                currentUser.IsCurrent = false;
        }
        private void ThrowIfUserNameIsInvalid(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName) || userName.Length < ModelConstants.UserNameMinLength || userName.Length > ModelConstants.UserNameMaxLength)
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.PropertyInvalidValue(nameof(EasyLearnUser.Name), nameof(EasyLearn), userName));
        }
        #endregion
    }
}
