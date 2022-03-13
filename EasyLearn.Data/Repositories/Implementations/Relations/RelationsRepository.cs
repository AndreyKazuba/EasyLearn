using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class RelationsRepository : IRelationsRepository
    {
        private readonly EasyLearnDbContext context;

        public RelationsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }
    }
}
