using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class VerbPrepositionsRepository : IVerbPrepositionsRepository
    {
        private readonly EasyLearnDbContext context;

        public VerbPrepositionsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }
    }
}
