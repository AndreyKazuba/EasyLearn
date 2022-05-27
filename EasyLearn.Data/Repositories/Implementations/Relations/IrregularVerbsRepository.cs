using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        #endregion
    }
}
