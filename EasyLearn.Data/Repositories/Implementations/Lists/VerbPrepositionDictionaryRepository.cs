﻿using EasyLearn.Data.Models;
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
        private readonly IEasyLearnUsersRerository usersRerository;

        public VerbPrepositionDictionaryRepository(EasyLearnContext context, IEasyLearnUsersRerository usersRerository)
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

        public IEnumerable<VerbPrepositionDictionnary> GetUsersVerbPreposotionDictionaries(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries.Where(dictionary => dictionary.UserId == dictionaryId).AsNoTracking();
        }

        public VerbPrepositionDictionnary? GetVerbPrepositionDictionary(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries.AsNoTracking().FirstOrDefault(dictionary => dictionary.Id == dictionaryId);
        }

        public Task<VerbPrepositionDictionnary?> GetVerbPrepositionDictionaryAsync(int dictionaryId)
        {
            return context.VerbPrepositionDictionaries.AsNoTracking().FirstOrDefaultAsync(dictionary => dictionary.Id == dictionaryId);
        }
    }
}
