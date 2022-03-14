using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class IrregularVerbsRepository : IIrregularVerbsRepository
    {
        private readonly EasyLearnDbContext context;

        public IrregularVerbsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<IrregularVerb> GetAllIrregularVerbs()
        {
            return context.IrregularVerbs.AsNoTracking();
        }
    }
}
