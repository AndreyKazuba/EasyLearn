using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionsRepository : IVerbPrepositionRepository
    {
        private readonly EasyLearnContext context;

        public VerbPrepositionsRepository(EasyLearnContext context)
        {
            this.context = context;
        }
    }
}
