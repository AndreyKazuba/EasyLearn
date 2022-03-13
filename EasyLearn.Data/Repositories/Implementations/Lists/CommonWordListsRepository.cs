using EasyLearn.Data.Repositories.Interfaces;

namespace EasyLearn.Data.Repositories.Implementations
{
    public class CommonWordListsRepository : ICommonWordListsRepository
    {
        private readonly EasyLearnDbContext context;

        public CommonWordListsRepository(EasyLearnDbContext context)
        {
            this.context = context;
        }
    }
}
