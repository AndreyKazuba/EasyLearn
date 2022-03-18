using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class CommonDictionaryRepository : ICommonDictionaryRepository
    {
        private readonly EasyLearnContext context;
        private readonly IEasyLearnUsersRerository usersRerository;

        public CommonDictionaryRepository(EasyLearnContext context, IEasyLearnUsersRerository usersRerository)
        {
            this.context = context;
            this.usersRerository = usersRerository;
        }

        public async Task<CommonDictionary?> CreateCommonDictionary(string name, string description, int userId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return null;
            }

            if (!usersRerository.IsUserExist(userId))
            {
                return null;
            }

            CommonDictionary newList = new CommonDictionary
            {
                Name = name,
                Description = description,
                UserId = userId,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.CommonDictionaries.Add(newList);
            await context.SaveChangesAsync();

            return newList;
        }

        public CommonDictionary GetCommonDictionary(int dictionaryId)
        {
            return context.CommonDictionaries
                .Include(commonDictionary => commonDictionary.Relations).ThenInclude(commonRelation => commonRelation.EnglishUnit)
                .Include(commonDictionary => commonDictionary.Relations).ThenInclude(commonRelatiob => commonRelatiob.RussianUnit)
                .AsNoTracking()
                .First(commonDictionary => commonDictionary.Id == dictionaryId);
        }

        public async Task<CommonDictionary> GetCommonDictionaryAsync(int dictionaryId)
        {
            return await context.CommonDictionaries
                .Include(commonDictionary => commonDictionary.Relations).ThenInclude(commonRelation => commonRelation.EnglishUnit)
                .Include(commonDictionary => commonDictionary.Relations).ThenInclude(commonRelatiob => commonRelatiob.RussianUnit)
                .AsNoTracking()
                .FirstAsync(commonDictionary => commonDictionary.Id == dictionaryId);
        }

        public IEnumerable<CommonDictionary> GetUsersCommonDictionaries(int userId)
        {
            return context.CommonDictionaries.Where(commonDictionary => commonDictionary.UserId == userId).AsNoTracking();
        }

        public bool IsCommonDictionaryExist(int dictionaryId)
        {
            return context.CommonDictionaries.Any(commonDictionary => commonDictionary.Id == dictionaryId);
        }
    }
}
