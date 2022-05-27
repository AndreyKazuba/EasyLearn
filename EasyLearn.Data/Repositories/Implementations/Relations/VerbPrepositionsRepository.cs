using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLearn.Data.Constants;
using EasyLearn.Data.DTO;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionsRepository : Repository, IVerbPrepositionRepository
    {
        #region Private fields
        private readonly IEnglishUnitRepository englishUnitRepository;
        private readonly IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository;
        #endregion

        public VerbPrepositionsRepository(EasyLearnContext context,
            IEnglishUnitRepository englishUnitRepository,
            IVerbPrepositionDictionaryRepository verbPrepositionDictionaryRepository) : base(context)
        {
            this.englishUnitRepository = englishUnitRepository;
            this.verbPrepositionDictionaryRepository = verbPrepositionDictionaryRepository;
        }

        #region Public members
        public bool IsVerbPrepositionExist(int verbId, int prepositionId, int dictionaryId) => context.VerbPrepositions.Any(verbPreposition => verbPreposition.VerbId == verbId && verbPreposition.PrepositionId == prepositionId && verbPreposition.VerbPrepositionDictionaryId == dictionaryId);
        public bool IsVerbPrepositionExist(string verbValue, string prepositionValue, int dictionaryId)
        {
            EnglishUnit? verb = englishUnitRepository.TryGetUnit(verbValue, UnitType.Verb);
            EnglishUnit? preposition = englishUnitRepository.TryGetUnit(prepositionValue, UnitType.Preposition);
            if (verb is null || preposition is null)
                return false;
            return IsVerbPrepositionExist(verb.Id, preposition.Id, dictionaryId);
        }
        public VerbPreposition GetVerbPreposition(int verbPrepositionId) => context.VerbPrepositions
           .Include(verbPreposition => verbPreposition.Verb)
           .Include(verbPreposition => verbPreposition.Preposition)
           .First(verbPreposition => verbPreposition.Id == verbPrepositionId);
        public async Task<VerbPreposition> CreateVerbPreposition(
            string verbValue,
            string prepositionValue,
            int dictionaryId,
            string translation,
            string? firstExampleRussianValue,
            string? firstExampleEnglshValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            EnglishUnit verb = await englishUnitRepository.GetOrCreateUnit(verbValue, UnitType.Verb);
            EnglishUnit preposition = await englishUnitRepository.GetOrCreateUnit(prepositionValue, UnitType.Preposition);
            ThrowIfAddingAttemptIncorrect(dictionaryId, verb.Id, preposition.Id, translation, firstExampleRussianValue, firstExampleEnglshValue, secondExampleRussianValue, secondExampleEnglishValue);
            VerbPreposition newVerbPreposition = new VerbPreposition
            {
                VerbId = verb.Id,
                PrepositionId = preposition.Id,
                Translation = StringHelper.Prepare(translation),
                CreationDateUtc = DateTime.UtcNow,
                VerbPrepositionDictionaryId = dictionaryId,
                FirstExampleRussianValue = StringHelper.TryPrepare(firstExampleRussianValue),
                FirstExampleEnglishValue = StringHelper.TryPrepare(firstExampleEnglshValue),
                SecondExampleRussianValue = StringHelper.TryPrepare(secondExampleRussianValue),
                SecondExampleEnglishValue = StringHelper.TryPrepare(secondExampleEnglishValue),
            };
            context.VerbPrepositions.Add(newVerbPreposition);
            await context.SaveChangesAsync();
            newVerbPreposition.Verb = verb;
            newVerbPreposition.Preposition = preposition;
            return newVerbPreposition;
        }
        public async Task<VerbPreposition> UpdateVerbPreposition(
            int verbPrepositionId,
            string translation,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            ThrowIfExampleValueInvalid(firstExampleEnglishValue, nameof(CommonRelation.FirstExampleEnglishValue));
            ThrowIfExampleValueInvalid(firstExampleRussianValue, nameof(CommonRelation.FirstExampleRussianValue));
            ThrowIfExampleValueInvalid(secondExampleEnglishValue, nameof(CommonRelation.SecondExampleEnglishValue));
            ThrowIfExampleValueInvalid(secondExampleRussianValue, nameof(CommonRelation.SecondExampleRussianValue));
            ThrowIfTranslationInvalid(translation);
            VerbPreposition updatedVerbPreposition = GetVerbPreposition(verbPrepositionId);
            updatedVerbPreposition.UpdateDateUtc = DateTime.UtcNow;
            updatedVerbPreposition.Translation = StringHelper.Prepare(translation);
            updatedVerbPreposition.FirstExampleRussianValue = StringHelper.TryPrepare(firstExampleRussianValue);
            updatedVerbPreposition.FirstExampleEnglishValue = StringHelper.TryPrepare(firstExampleEnglishValue);
            updatedVerbPreposition.SecondExampleRussianValue = StringHelper.TryPrepare(secondExampleRussianValue);
            updatedVerbPreposition.SecondExampleEnglishValue = StringHelper.TryPrepare(secondExampleEnglishValue);
            await context.SaveChangesAsync();
            context.ChangeTracker.Clear();
            return updatedVerbPreposition;
        }
        public async Task DeleteAllDictionaryVerbPrepositions(int dictionaryId)
        {
            IEnumerable<VerbPreposition> verbPrepositions = context.VerbPrepositions.Where(verbPreposition => verbPreposition.VerbPrepositionDictionaryId == dictionaryId);
            context.VerbPrepositions.RemoveRange(verbPrepositions);
            await context.SaveChangesAsync();
        }
        public async Task DeleteVerbPreposition(int verbPrepositionId)
        {
            VerbPreposition verbPreposition = await context.VerbPrepositions.FirstAsync(verbPreposition => verbPreposition.Id == verbPrepositionId);
            context.VerbPrepositions.Remove(verbPreposition);
            await context.SaveChangesAsync();
        }
        public async void SaveDictationResults(List<Answer> answers)
        {
            List<VerbPreposition> verbPrepositions = await context.VerbPrepositions.Where(verbPreposition => answers.Any(answer => answer.RelationId == verbPreposition.Id)).ToListAsync();
            foreach (VerbPreposition verbPreposition in verbPrepositions)
            {
                if (verbPreposition.Studied)
                    continue;
                Answer answer = answers.First(answer => answer.RelationId == verbPreposition.Id);
                int updatedRating = NumberHelper.GetRangedValue(verbPreposition.Rating + answer.Variation.GetAnswerSignificanceValue(), ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue);
                int correctAnswersStreak = verbPreposition.CorrectAnswersStreak;
                bool studied = false;
                if (updatedRating == ModelConstants.RatingMaxValue)
                    correctAnswersStreak++;
                else
                    correctAnswersStreak = 0;
                if (correctAnswersStreak == ModelConstants.CorrectAnswersStreakMaxValue)
                    studied = true;
                verbPreposition.Rating = updatedRating;
                verbPreposition.CorrectAnswersStreak = correctAnswersStreak;
                verbPreposition.Studied = studied;
            }
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(
            int dictionaryId,
            int verbId,
            int prepositionId,
            string translation,
            string? firstExampleRussianValue,
            string? firstExampleEnglishValue,
            string? secondExampleRussianValue,
            string? secondExampleEnglishValue)
        {
            ThrowIfExampleValueInvalid(firstExampleEnglishValue, nameof(CommonRelation.FirstExampleEnglishValue));
            ThrowIfExampleValueInvalid(firstExampleRussianValue, nameof(CommonRelation.FirstExampleRussianValue));
            ThrowIfExampleValueInvalid(secondExampleEnglishValue, nameof(CommonRelation.SecondExampleEnglishValue));
            ThrowIfExampleValueInvalid(secondExampleRussianValue, nameof(CommonRelation.SecondExampleRussianValue));
            ThrowIfTranslationInvalid(translation);
            if (IsVerbPrepositionExist(dictionaryId, verbId, prepositionId))
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.AttemptToAddExistingEntity(nameof(VerbPreposition), nameof(VerbPreposition.PrepositionId), prepositionId.ToString(), nameof(VerbPreposition.VerbId), verbId.ToString(), nameof(VerbPreposition.VerbPrepositionDictionaryId), dictionaryId.ToString()));
            if (!verbPrepositionDictionaryRepository.IsVerbPrepositionDictionaryExist(dictionaryId))
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.AddingForNonExistingEntity(nameof(VerbPreposition), nameof(VerbPrepositionDictionnary), dictionaryId.ToString()));
        }
        private void ThrowIfTranslationInvalid(string translation)
        {
            if (string.IsNullOrEmpty(translation) || StringHelper.IsEmptyOrWhiteSpace(translation) || translation.Length > ModelConstants.VerbPrepositionTranslationMaxLength || translation.Length < ModelConstants.VerbPrepositionTranslationMinLength)
                throw new InvalidDbOperationException(DbExceptionMessagesHelper.PropertyInvalidValue(nameof(VerbPreposition.Translation), nameof(VerbPreposition), translation));
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
