namespace EasyLearn.Data.Repositories
{
    public abstract class Repository
    {
        protected EasyLearnContext context;
        public Repository(EasyLearnContext context) { this.context = context; }
    }
}
