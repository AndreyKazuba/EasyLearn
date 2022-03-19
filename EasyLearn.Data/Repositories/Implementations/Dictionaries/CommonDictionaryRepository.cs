﻿using System;
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
    public class CommonDictionaryRepository : Repository, ICommonDictionaryRepository
    {
        #region Private fields
        private readonly IEasyLearnUserRepository userRepository;
        #endregion

        public CommonDictionaryRepository(EasyLearnContext context, IEasyLearnUserRepository userRerository) : base(context)
        {
            this.userRepository = userRerository;
        }

        #region Public members
        public bool IsCommonDictionaryExist(int dictionaryId) => context.CommonDictionaries.Any(dictionary => dictionary.Id == dictionaryId);
        public CommonDictionary GetCommonDictionary(int dictionaryId)
        {
            return context.CommonDictionaries
                .Include(dictionary => dictionary.Relations).ThenInclude(commonRelation => commonRelation.EnglishUnit)
                .Include(dictionary => dictionary.Relations).ThenInclude(commonRelatiob => commonRelatiob.RussianUnit)
                .AsNoTracking()
                .First(dictionary => dictionary.Id == dictionaryId);
        }
        public async Task<CommonDictionary> GetCommonDictionaryAsync(int dictionaryId)
        {
            return await context.CommonDictionaries
                .Include(dictionary => dictionary.Relations).ThenInclude(commonRelation => commonRelation.EnglishUnit)
                .Include(dictionary => dictionary.Relations).ThenInclude(commonRelatiob => commonRelatiob.RussianUnit)
                .AsNoTracking()
                .FirstAsync(dictionary => dictionary.Id == dictionaryId);
        }
        public IEnumerable<CommonDictionary> GetUsersCommonDictionaries(int userId) => context.CommonDictionaries.Where(dictionary => dictionary.UserId == userId).AsNoTracking();
        public async Task<CommonDictionary> CreateCommonDictionary(string name, string description, int userId)
        {
            ThrowIfAddingAttemptIncorrect(name, description, userId);
            CommonDictionary newList = new CommonDictionary
            {
                Name = StringHelper.Prepare(name),
                Description = StringHelper.Prepare(description),
                UserId = userId,
                CreationDateUtc = DateTime.UtcNow,
            };
            context.CommonDictionaries.Add(newList);
            await context.SaveChangesAsync();
            return newList;
        }
        public async Task DeleteCommonDictionary(int dictionaryId)
        {
            CommonDictionary commonDictionary = context.CommonDictionaries.First(dictionary => dictionary.Id == dictionaryId);
            context.CommonDictionaries.Remove(commonDictionary);
            await context.SaveChangesAsync();
        }
        public async Task EditCommonDictionary(int dictionaryId, string name, string description)
        {
            ThrowIfEditingAttemptIncorrect(dictionaryId, name, description);
            CommonDictionary commonDictionary = await context.CommonDictionaries.FirstAsync(dictionary => dictionary.Id == dictionaryId);
            commonDictionary.Description = StringHelper.Prepare(description);
            commonDictionary.Name = StringHelper.Prepare(name);
            commonDictionary.ChangeDateUtc = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private void ThrowIfEditingAttemptIncorrect(int dictionaryId, string name, string description)
        {
            ThrowIfDictionaryNameInvalid(name);
            ThrowIfDictionaryDescriptionInvalid(description);
        }
        private void ThrowIfAddingAttemptIncorrect(string name, string description, int userId)
        {
            ThrowIfDictionaryNameInvalid(name);
            ThrowIfDictionaryDescriptionInvalid(description);
            if (!userRepository.IsUserExist(userId))
                throw new InvalidDbOperationException($"Попытка добавить {nameof(CommonDictionary)} несуществующему {nameof(EasyLearnUser)} с {nameof(EasyLearnUser.Id)} = '{userId}'");
        }
        private void ThrowIfDictionaryNameInvalid(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || name.Length < ModelConstants.DictionaryNameMinLength || name.Length > ModelConstants.DictionaryNameMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(CommonDictionary.Name), nameof(CommonDictionary), name));
        }
        private void ThrowIfDictionaryDescriptionInvalid(string description)
        {
            if (string.IsNullOrWhiteSpace(description) || description.Length < ModelConstants.DictionaryDescriptionMinLength || description.Length > ModelConstants.DictionaryDescriptionMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(CommonDictionary.Description), nameof(CommonDictionary), description));
        }
        #endregion
    }
}
