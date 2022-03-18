using EasyLearn.Data.Models;
using EasyLearn.Data.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionsRepository : IVerbPrepositionsRepository
    {
        private readonly EasyLearnContext context;

        public VerbPrepositionsRepository(EasyLearnContext context)
        {
            this.context = context;
        }
    }
}
