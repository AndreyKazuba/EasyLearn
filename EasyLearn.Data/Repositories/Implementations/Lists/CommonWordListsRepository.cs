using EasyLearn.Data.Enums;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class CommonWordListsRepository : ICommonWordListsRepository
    {
        private readonly EasyLearnDbContext context;
        private readonly IEasyLearnUsersRerository usersRerository;

        public CommonWordListsRepository(EasyLearnDbContext context, IEasyLearnUsersRerository usersRerository)
        {
            this.context = context;
            this.usersRerository = usersRerository;
        }

        public async Task AddList(string name, string description, int userId, CommonWordListType type)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return;
            }

            if (!usersRerository.IsUserExist(userId))
            {
                return;
            }

            CommonWordList newList = new CommonWordList
            {
                Name = name,
                Description = description,
                UserId = userId,
                Type = type,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.CommonWordLists.Add(newList);
            await context.SaveChangesAsync();
        }

        public CommonWordList GetCommonWordList(int listId)
        {
            return context.CommonWordLists
                .Include(list => list.Relations)
                .First(list => list.Id == listId);
        }

        public async Task<CommonWordList> GetCommonWordListAsync(int listId)
        {
            return await context.CommonWordLists
                .Include(list => list.Relations)
                .FirstAsync(list => list.Id == listId);
        }

        public IEnumerable<CommonWordList> GetUsersCommonLists(int userId)
        {
            return context.CommonWordLists.Where(list => list.UserId == userId).AsNoTracking();
        }

        public bool IsWordListExist(int userListId)
        {
            return context.CommonWordLists.Any(list => list.Id == userListId);
        }
    }
}
