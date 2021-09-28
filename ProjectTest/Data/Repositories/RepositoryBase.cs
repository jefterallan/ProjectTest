using ProjectTest.Data;
using ProjectTest.Data.Repositories.Interfaces;

namespace ProjectTest.Data.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly ProjectTestContext Context;

        protected RepositoryBase(ProjectTestContext context)
        {
            Context = context;
        }
    }
}
