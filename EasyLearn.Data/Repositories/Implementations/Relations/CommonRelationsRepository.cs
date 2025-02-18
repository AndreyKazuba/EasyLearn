﻿using EasyLearn.Data.Constants;
using EasyLearn.Data.DTO;
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
        public bool IsCommonRelationExist(string russianUnitValue, UnitType russianUnitType, string eglishUnitValue, UnitType englishUnitType, int dictionaryId)
        {
            EnglishUnit? englishUnit = englishUnitRepository.TryGetUnit(eglishUnitValue, englishUnitType);
            RussianUnit? russianUnit = russianUnitRepository.TryGetUnit(russianUnitValue, russianUnitType);
            if (englishUnit is null || russianUnit is null)
                return false;
            return IsCommonRelationExist(russianUnit.Id, englishUnit.Id, dictionaryId);
        }
        public CommonRelation GetCommonRelation(int commonRelationId) => context.CommonRelations
           .Include(commonRelation => commonRelation.RussianUnit)
           .Include(commonRelation => commonRelation.EnglishUnit)
           .First(commonRelation => commonRelation.Id == commonRelationId);
        public async Task<CommonRelation> GetCommonRelationAsync(int commonRelationId) => await context.CommonRelations
            .Include(commonRelation => commonRelation.RussianUnit)
            .Include(commonRelation => commonRelation.EnglishUnit)
            .FirstAsync(commonRelation => commonRelation.Id == commonRelationId);
        public async Task<CommonRelation> CreateCommonRelation(
            string russianUnitValue,
            UnitType russianUnitType,
            string englishUnitValue,
            UnitType englishUnitType,
            int dictionaryId,
            string? comment,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            EnglishUnit englishUnit = await englishUnitRepository.GetOrCreateUnit(englishUnitValue, englishUnitType);
            RussianUnit russianUnit = await russianUnitRepository.GetOrCreateUnit(russianUnitValue, russianUnitType);
            ThrowIfAddingAttemptIncorrect(dictionaryId, comment, russianUnit.Id, englishUnit.Id, firstExampleRussianValue, firstExampleEnglishValue, secondExampleRussianValue, secondExampleEnglishValue);
            CommonRelation newCommonRelation = new CommonRelation
            {
                EnglishUnitId = englishUnit.Id,
                RussianUnitId = russianUnit.Id,
                CreationDateUtc = DateTime.UtcNow,
                CommonDictionaryId = dictionaryId,
                Comment = StringHelper.TryPrepare(comment),
                FirstExampleRussianValue = StringHelper.TryPrepare(firstExampleRussianValue),
                FirstExampleEnglishValue = StringHelper.TryPrepare(firstExampleEnglishValue),
                SecondExampleRussianValue = StringHelper.TryPrepare(secondExampleRussianValue),
                SecondExampleEnglishValue = StringHelper.TryPrepare(secondExampleEnglishValue),
            };
            context.CommonRelations.Add(newCommonRelation);
            await context.SaveChangesAsync();
            newCommonRelation.RussianUnit = russianUnit;
            newCommonRelation.EnglishUnit = englishUnit;
            context.ChangeTracker.Clear();
            return newCommonRelation;
        }
        public async Task<CommonRelation> UpdateCommonRelation(
            int commonRelationId,
            string? comment,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            ThrowIfExampleValueInvalid(firstExampleEnglishValue, nameof(CommonRelation.FirstExampleEnglishValue));
            ThrowIfExampleValueInvalid(firstExampleRussianValue, nameof(CommonRelation.FirstExampleRussianValue));
            ThrowIfExampleValueInvalid(secondExampleEnglishValue, nameof(CommonRelation.SecondExampleEnglishValue));
            ThrowIfExampleValueInvalid(secondExampleRussianValue, nameof(CommonRelation.SecondExampleRussianValue));
            ThrowIfCommentInvalid(comment);
            CommonRelation updatedCommonRelation = GetCommonRelation(commonRelationId);
            updatedCommonRelation.UpdateDateUtc = DateTime.UtcNow;
            updatedCommonRelation.Comment = StringHelper.TryPrepare(comment);
            updatedCommonRelation.FirstExampleRussianValue = StringHelper.TryPrepare(firstExampleRussianValue);
            updatedCommonRelation.FirstExampleEnglishValue = StringHelper.TryPrepare(firstExampleEnglishValue);
            updatedCommonRelation.SecondExampleRussianValue = StringHelper.TryPrepare(secondExampleRussianValue);
            updatedCommonRelation.SecondExampleEnglishValue = StringHelper.TryPrepare(secondExampleEnglishValue);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return updatedCommonRelation;
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
        public void SaveDictationResults(List<Answer> answers)
        {
            foreach (Answer answer in answers)
            {
                CommonRelation commonRelation = context.CommonRelations.First(commonRelation => commonRelation.Id == answer.RelationId);
                commonRelation.LastRepetitionDateUtc = DateTime.UtcNow;
                if (commonRelation.Studied)
                    continue;
                int updatedRating = NumberHelper.GetRangedValue(commonRelation.Rating + answer.Variation.GetAnswerSignificanceValue(), ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue);
                int correctAnswersStreak = commonRelation.CorrectAnswersStreak;
                bool studied = false;
                if (updatedRating == ModelConstants.RatingMaxValue)
                    correctAnswersStreak++;
                else
                    correctAnswersStreak = 0;
                if (correctAnswersStreak == ModelConstants.CorrectAnswersStreakMaxValue)
                    studied = true;
                commonRelation.Rating = updatedRating;
                commonRelation.CorrectAnswersStreak = correctAnswersStreak;
                commonRelation.Studied = studied;
            }
            context.SaveChanges();
        }
        public CommonRelation ResetCommonRelationRating(int commonRelationId)
        {
            CommonRelation commonRelation = GetCommonRelation(commonRelationId);
            commonRelation.Rating = 0;
            commonRelation.CorrectAnswersStreak = 0;
            commonRelation.Studied = false;
            context.SaveChanges();
            return commonRelation;
        }
        #endregion

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(
            int dictionaryId,
            string? comment,
            int russianUnitId,
            int englishUnitId,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            ThrowIfExampleValueInvalid(firstExampleEnglishValue, nameof(CommonRelation.FirstExampleEnglishValue));
            ThrowIfExampleValueInvalid(firstExampleRussianValue, nameof(CommonRelation.FirstExampleRussianValue));
            ThrowIfExampleValueInvalid(secondExampleEnglishValue, nameof(CommonRelation.SecondExampleEnglishValue));
            ThrowIfExampleValueInvalid(secondExampleRussianValue, nameof(CommonRelation.SecondExampleRussianValue));
            ThrowIfCommentInvalid(comment);
            if (IsCommonRelationExist(russianUnitId, englishUnitId, dictionaryId))
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.AttemptToAddExistingEntity(nameof(CommonRelation), nameof(CommonRelation.RussianUnitId), russianUnitId.ToString(), nameof(CommonRelation.EnglishUnitId), englishUnitId.ToString(), nameof(CommonRelation.CommonDictionaryId), dictionaryId.ToString()));
            if (!commonDictionaryRepository.IsCommonDictionaryExist(dictionaryId))
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.AddingForNonExistingEntity(nameof(CommonRelation), nameof(CommonDictionary), dictionaryId.ToString()));
        }
        private void ThrowIfCommentInvalid(string? comment)
        {
            if (comment is null)
                return;
            if (StringHelper.IsEmptyOrWhiteSpace(comment) || comment.Length > ModelConstants.CommonRelationCommentMaxLength)
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.PropertyInvalidValue(nameof(CommonRelation.Comment), nameof(CommonRelation), comment));
        }
        private void ThrowIfExampleValueInvalid(string? exampleValue, string propName)
        {
            if (exampleValue is null)
                return;
            if (StringHelper.IsEmptyOrWhiteSpace(exampleValue) || exampleValue.Length > ModelConstants.ExampleValueMaxLength)
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.PropertyInvalidValue(propName, nameof(CommonRelation), exampleValue));
        }
        #endregion
    }
}
