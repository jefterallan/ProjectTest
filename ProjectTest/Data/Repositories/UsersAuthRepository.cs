using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories.Interfaces;
using System.Linq;

namespace ProjectTest.Data.Repositories
{
    public class UsersAuthRepository : RepositoryBase<TblUsersAuth>, IUsersAuthRepository
    {
        public UsersAuthRepository(ProjectTestContext context) : base(context)
        {
        }

        public bool ValidateCredentials(TblUsersAuth userAuth)
        {
            return Context.TblUsersAuths.Any(u => u.User == userAuth.User && u.Password == userAuth.Password);
        }
    }
}
