using EasyLearn.Data.Constants;
using EasyLearn.Data.DTO;
using EasyLearn.Data.Helpers;
using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class IrregularVerbsRepository : Repository, IIrregularVerbRepository
    {
        public IrregularVerbsRepository(EasyLearnContext context) : base(context) { }

        #region Public members
        public IEnumerable<IrregularVerb> GetAllIrregularVerbs()
        {
            return context.IrregularVerbs
                .Include(irregularVerb => irregularVerb.RussianUnit)
                .Include(irregularVerb => irregularVerb.FirstForm)
                .Include(irregularVerb => irregularVerb.SecondForm)
                .Include(irregularVerb => irregularVerb.ThirdForm)
                .AsNoTracking();
        }
        public void SaveDictationResults(List<Answer> answers)
        {
            foreach (Answer answer in answers)
            {
                IrregularVerb irregularVerb = context.IrregularVerbs.First(irregularVerb => irregularVerb.Id == answer.RelationId);
                int updatedRating = NumberHelper.GetRangedValue(irregularVerb.Rating + answer.Variation.GetAnswerSignificanceValue(), ModelConstants.RatingMinValue, ModelConstants.RatingMaxValue);
                irregularVerb.Rating = updatedRating;
            }
            context.SaveChanges();
        }
        #endregion
    }
}
