using ProjectTest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTest.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task StoreData(TblUser user);
        Task<IEnumerable<TblUser>> GetData();
    }
}
