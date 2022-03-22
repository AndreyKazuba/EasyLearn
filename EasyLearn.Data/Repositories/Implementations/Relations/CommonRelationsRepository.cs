using EasyLearn.Data.Enums;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class CommonRelationsRepository : Repository, ICommonRelationRepository
    {
        #region Private fields
        private readonly IEnglishUnitRepository englishUnitRepository;
        private readonly IRussianUnitRepository russianUnitRepository;
        private readonly ICommonDictionaryRepository commonDictionaryRepository;
        #endregion

        public CommonRelationsRepository(EasyLearnContext context,
            IRussianUnitRepository russianUnitRepository,
            IEnglishUnitRepository englishUnitRepository,
            ICommonDictionaryRepository commonDictionaryRepository) : base(context)
        {
            this.englishUnitRepository = englishUnitRepository;
            this.russianUnitRepository = russianUnitRepository;
            this.commonDictionaryRepository = commonDictionaryRepository;
        }

        #region Public members
        public bool IsCommonRelationExist(int russianUnitId, int englishUnitId, int dictionaryId) => context.CommonRelations.Any(relation => relation.RussianUnitId == russianUnitId && relation.EnglishUnitId == englishUnitId && relation.CommonDictionaryId == dictionaryId);
        public async Task<CommonRelation> CreateCommonRelation(string russianUnitValue, UnitType russianUnitType, string englishUnitValue, UnitType englishUnitType, int dictionaryId, string? comment)
        {
            EnglishUnit englishUnit = await englishUnitRepository.GetOrCreateUnit(englishUnitValue, englishUnitType);
            RussianUnit russianUnit = await russianUnitRepository.GetOrCreateUnit(russianUnitValue, russianUnitType);
            ThrowIfAddingAttemptIncorrect(dictionaryId, comment, russianUnit.Id, englishUnit.Id);
            CommonRelation newCommonRelation = new CommonRelation
            {
                EnglishUnitId = englishUnit.Id,
                RussianUnitId = russianUnit.Id,
                CreationDateUtc = DateTime.UtcNow,
                CommonDictionaryId = dictionaryId,
                Comment = StringHelper.TryPrepare(comment),
            };
            context.CommonRelations.Add(newCommonRelation);
            await context.SaveChangesAsync();
            newCommonRelation.RussianUnit = russianUnit;
            newCommonRelation.EnglishUnit = englishUnit;
            return newCommonRelation;
        }
        public async Task DeleteAllDictionaryRelations(int dictionaryId)
        {
            IEnumerable<CommonRelation> commonRelations = context.CommonRelations.Where(relation => relation.CommonDictionaryId == dictionaryId);
            context.CommonRelations.RemoveRange(commonRelations);
            await context.SaveChangesAsync();
        }
        public async Task DeleteCommonRelation(int commonRelationId)
        {
            CommonRelation commonRelation = await context.CommonRelations.FirstAsync(commonRelation => commonRelation.Id == commonRelationId);
            context.CommonRelations.Remove(commonRelation);
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(int dictionaryId, string? comment, int russianUnitId, int englishUnitId)
        {
            ThrowIfCommentInvalid(comment);
            if (IsCommonRelationExist(russianUnitId, englishUnitId, dictionaryId))
                throw new InvalidDbOperationException($"Попытка добавить уже существующий {nameof(CommonRelation)}");
            if (!commonDictionaryRepository.IsCommonDictionaryExist(dictionaryId))
                throw new InvalidDbOperationException($"Попытка добавить {nameof(CommonRelation)} в несуществующий {nameof(CommonDictionary)} c {nameof(CommonDictionary.Id)} = '{dictionaryId}'");
        }
        private void ThrowIfCommentInvalid(string? comment)
        {
            if (comment is null)
                return;
            if (StringHelper.IsEmptyOrWhiteSpace(comment) || comment.Length < ModelConstants.RelationCommentMinLength || comment.Length > ModelConstants.RelationCommentMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(CommonRelation.Comment), nameof(CommonRelation), comment));
        }
        #endregion
    }
}
