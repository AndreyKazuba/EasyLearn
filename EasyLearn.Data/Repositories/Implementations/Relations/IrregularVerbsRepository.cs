using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class IrregularVerbsRepository : IIrregularVerbsRepository
    {
        private readonly EasyLearnDbContext context;

        public IrregularVerbsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }
    }
}
