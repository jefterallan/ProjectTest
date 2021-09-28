using ProjectTest.Data.Models;

namespace ProjectTest.Data.Repositories.Interfaces
{
    public interface IUsersAuthRepository
    {
        bool ValidateCredentials(TblUsersAuth userAuth);
    }
}
