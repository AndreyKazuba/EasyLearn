using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLearn.Data.Enums;
using EasyLearn.Data.Exceptions;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;

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
        public async Task<VerbPreposition> CreateVerbPreposition(string verbValue, string prepositionValue, int dictionaryId, string? comment)
        {
            EnglishUnit verb = await englishUnitRepository.GetOrCreateUnit(verbValue, UnitType.Verb);
            EnglishUnit preposition = await englishUnitRepository.GetOrCreateUnit(prepositionValue, UnitType.Preposition);
            ThrowIfAddingAttemptIncorrect(dictionaryId, comment, verb.Id, preposition.Id);
            VerbPreposition newVerbPreposition = new VerbPreposition
            {
                VerbId = verb.Id,
                PrepositionId = preposition.Id,
                Comment = StringHelper.TryPrepare(comment),
                CreationDateUtc = DateTime.UtcNow,
                VerbPrepositionDictionaryId = dictionaryId,
            };
            context.VerbPrepositions.Add(newVerbPreposition);
            await context.SaveChangesAsync();
            return newVerbPreposition;
        }
        public async Task DeleteAllDictionaryVerbPrepositions(int dictionaryId)
        {
            IEnumerable<VerbPreposition> verbPrepositions = context.VerbPrepositions.Where(verbPreposition => verbPreposition.VerbPrepositionDictionaryId == dictionaryId);
            context.VerbPrepositions.RemoveRange(verbPrepositions);
            await context.SaveChangesAsync();
        }
        #endregion

        #region Private members
        private void ThrowIfAddingAttemptIncorrect(int dictionaryId, string? comment, int verbId, int prepositionId)
        {
            ThrowIfCommentInvalid(comment);
            if (IsVerbPrepositionExist(dictionaryId, verbId, prepositionId))
                throw new InvalidDbOperationException($"Попытка добавить уже существующий {nameof(VerbPreposition)}");
            if (!verbPrepositionDictionaryRepository.IsVerbPrepositionDictionaryExist(dictionaryId))
                throw new InvalidDbOperationException($"Попытка добавить {nameof(VerbPreposition)} в несуществующий {nameof(VerbPrepositionDictionnary)} c {nameof(VerbPrepositionDictionnary.Id)} = '{dictionaryId}'");
        }
        private void ThrowIfCommentInvalid(string? comment)
        {
            if (comment is null)
                return;
            if (StringHelper.IsEmptyOrWhiteSpace(comment) || comment.Length < ModelConstants.RelationCommentMinLength || comment.Length > ModelConstants.RelationCommentMaxLength)
                throw new InvalidDbOperationException(ExceptionMessagesHelper.PropertyInvalidValue(nameof(VerbPreposition.Comment), nameof(VerbPreposition), comment));
        }
        #endregion
    }
}
