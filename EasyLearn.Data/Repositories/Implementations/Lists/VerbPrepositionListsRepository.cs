using EasyLearn.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionListsRepository : IVerbPrepositionListsRepository
    {
        private readonly EasyLearnDbContext context;

        public VerbPrepositionListsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }
    }
}
