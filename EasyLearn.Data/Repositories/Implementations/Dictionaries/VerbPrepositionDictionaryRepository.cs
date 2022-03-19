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
    public class VerbPrepositionDictionaryRepository : IVerbPrepositionDictionaryRepository
    {
        private readonly EasyLearnContext context;
        private readonly IEasyLearnUserRerository usersRerository;

        public VerbPrepositionDictionaryRepository(EasyLearnContext context, IEasyLearnUserRerository usersRerository)
        {
            this.context = context;
            this.usersRerository = usersRerository;
        }

        public async Task<VerbPrepositionDictionnary?> CreateVerbPrepositionDictionary(string name, string description, int userId)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(description))
            {
                return null;
            }

            if (!usersRerository.IsUserExist(userId))
            {
                return null;
            }

            VerbPrepositionDictionnary newVerbPrepositionDictionary = new VerbPrepositionDictionnary
            {
                Name = name,
                Description = description,
                UserId = userId,
                CreationDateUtc = DateTime.UtcNow,
            };

            context.VerbPrepositionDictionaries.Add(newVerbPrepositionDictionary);
            await context.SaveChangesAsync();

            return newVerbPrepositionDictionary;
        }

        public async Task DeleteVerbPrepositionDictionary(int dictionaryId)
        {
            VerbPrepositionDictionnary dictionnary = context.VerbPrepositionDictionaries.First(dictionary => dictionary.Id == dictionaryId);
            context.VerbPrepositionDictionaries.Remove(dictionnary);
            await context.SaveChangesAsync();
        }

        public IEnumerable<VerbPrepositionDictionnary> GetUsersVerbPreposotionDictionaries(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries.Where(dictionary => dictionary.UserId == dictionaryId).AsNoTracking();
        }

        public VerbPrepositionDictionnary GetVerbPrepositionDictionary(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries
                .Include(dictionary => dictionary.VerbPrepositions).ThenInclude(verbPreposition => verbPreposition.Verb)
                .Include(dictionary => dictionary.VerbPrepositions).ThenInclude(verbPreposition => verbPreposition.Preposition)
                .AsNoTracking()
                .First(dictionary => dictionary.Id == dictionaryId);
        }

        public Task<VerbPrepositionDictionnary> GetVerbPrepositionDictionaryAsync(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries
                .Include(dictionary => dictionary.VerbPrepositions).ThenInclude(verbPreposition => verbPreposition.Verb)
                .Include(dictionary => dictionary.VerbPrepositions).ThenInclude(verbPreposition => verbPreposition.Preposition)
                .AsNoTracking()
                .FirstAsync(dictionary => dictionary.Id == dictionaryId);
        }

        public bool IsVerbPrepositionDictionaryExist(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries.Any(dictionary => dictionary.Id == dictionaryId);
        }
    }
}
