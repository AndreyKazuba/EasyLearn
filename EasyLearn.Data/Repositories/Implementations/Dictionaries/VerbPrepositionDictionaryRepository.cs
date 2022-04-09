using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using EasyLearn.Data.Helpers;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionDictionaryRepository : Repository, IVerbPrepositionDictionaryRepository
    {
        #region Private fields
        private readonly IEasyLearnUserRepository userRepository;
        #endregion

        public VerbPrepositionDictionaryRepository(EasyLearnContext context, IEasyLearnUserRepository userRepository) : base(context)
        {
            this.userRepository = userRepository;
        }

        #region Public members
        public bool IsVerbPrepositionDictionaryExist(int dictionaryId) => context.VerbPrepositionDictionaries.Any(dictionary => dictionary.Id == dictionaryId);
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
        public IEnumerable<VerbPrepositionDictionnary> GetUsersVerbPreposotionDictionaries(int dictionaryId) => context.VerbPrepositionDictionaries.Where(dictionary => dictionary.UserId == dictionaryId).AsNoTracking();
        public async Task<VerbPrepositionDictionnary> CreateVerbPrepositionDictionary(string name, string? description, int userId)
        {
            ThrowIfAddingAttemptIncorrect(name, description, userId);
            VerbPrepositionDictionnary newVerbPrepositionDictionary = new VerbPrepositionDictionnary
            {
                Name = StringHelper.Prepare(name),
                Description = StringHelper.TryPrepare(description),
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
        public async Task EditVerbPrepositionDictionary(int dictionaryId, string name, string? description)
        {
            ThrowIfEditingAttemptIncorrect(name, description);
            VerbPrepositionDictionnary verbPrepositionDictionnary = await context.VerbPrepositionDictionaries.FirstAsync(dictionary => dictionary.Id == dictionaryId);
            verbPrepositionDictionnary.Description = StringHelper.TryPrepare(description);
            verbPrepositionDictionnary.Name = StringHelper.Prepare(name);
            verbPrepositionDictionnary.ChangeDateUtc = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private void ThrowIfEditingAttemptIncorrect(string name, string? description)
        {
            ThrowIfDictionaryNameInvalid(name);
            ThrowIfDictionaryDescriptionInvalid(description);
        }
        private void ThrowIfAddingAttemptIncorrect(string name, string? description, int userId)
        {
            ThrowIfDictionaryNameInvalid(name);
            ThrowIfDictionaryDescriptionInvalid(description);
            if (!userRepository.IsUserExist(userId))
                throw new InvalidDbOperationException($"Попытка добавить {nameof(VerbPrepositionDictionnary)} несуществующему {nameof(EasyLearnUser)} с {nameof(EasyLearnUser.Id)} = '{userId}'");
        }
        private void ThrowIfDictionaryNameInvalid(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < ModelConstants.DictionaryNameMinLength || name.Length > ModelConstants.DictionaryNameMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(VerbPrepositionDictionnary.Name), nameof(VerbPrepositionDictionnary), name));
        }
        private void ThrowIfDictionaryDescriptionInvalid(string? description)
        {
            if (description is null)
                return;
            if (StringHelper.IsEmptyOrWhiteSpace(description) || description.Length < ModelConstants.DictionaryDescriptionMinLength || description.Length > ModelConstants.DictionaryDescriptionMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(VerbPrepositionDictionnary.Description), nameof(VerbPrepositionDictionnary), description));
        }
        #endregion
    }
}