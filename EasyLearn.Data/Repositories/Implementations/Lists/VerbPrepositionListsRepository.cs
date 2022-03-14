using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionListsRepository : IVerbPrepositionListsRepository
    {
        private readonly EasyLearnDbContext context;
        private readonly IEasyLearnUsersRerository usersRerository;

        public VerbPrepositionListsRepository(EasyLearnDbContext context, IEasyLearnUsersRerository usersRerository)
        {
            this.context = context;
            this.usersRerository = usersRerository;
        }

        public async Task AddList(string name, string description, int userId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return;
            }

            if (!usersRerository.IsUserExist(userId))
            {
                return;
            }

            VerbPrepositionList newList = new VerbPrepositionList
            {
                Name = name,
                Description = description,
                UserId = userId,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.VerbPrepositionLists.Add(newList);
            await context.SaveChangesAsync();
        }

        public IEnumerable<VerbPrepositionList> GetUsersVerbPreposotionLists(int userId)
        {
            return context.VerbPrepositionLists.Where(list => list.UserId == userId).AsNoTracking();
        }
    }
}
