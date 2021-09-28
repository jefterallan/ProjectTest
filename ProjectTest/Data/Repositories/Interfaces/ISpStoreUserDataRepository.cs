using ProjectTest.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Data.Repositories.Interfaces
{
    public interface ISpStoreUserDataRepository
    {
        Task<List<SpStoreUserData>> StoreData(TblUser user);
    }
}
