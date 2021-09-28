using Microsoft.EntityFrameworkCore;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTest.Data.Repositories
{
    public class UserRepository : RepositoryBase<TblUser>, IUserRepository
    {
        public UserRepository(ProjectTestContext context) : base(context)
        {
        }

        public async Task StoreData(TblUser user)
        {
            await Context.TblUsers.AddAsync(user);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TblUser>> GetData()
        {
            return await Context.TblUsers.ToListAsync();
        }

        public async Task<TblUser> FindDuplicatedData(TblUser user)
        {
            return await Context.TblUsers.FirstOrDefaultAsync(u => u.Name == user.Name &&
                                                            u.Gender == user.Gender &&
                                                            u.Age == user.Age &&
                                                            u.Country == user.Country &&
                                                            u.City == user.City);
        }
    }
}
