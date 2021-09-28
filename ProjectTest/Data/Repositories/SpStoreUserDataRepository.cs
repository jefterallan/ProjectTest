using Microsoft.EntityFrameworkCore;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTest.Data.Repositories
{
    public class SpStoreUserDataRepository : RepositoryBase<SpStoreUserData>, ISpStoreUserDataRepository
    {
        private const string SQLCOMMAND = "EXEC [dbo].[SP_STORE_USER_DATA] {0},{1},{2},{3},{4},{5},{6}";

        public SpStoreUserDataRepository(ProjectTestContext context) : base(context)
        {
        }

        public async Task<List<SpStoreUserData>> StoreData(TblUser user)
        {
            return await Context.SpStoreUserData.
                FromSqlRaw(SQLCOMMAND, Guid.NewGuid(), user.Name, user.Gender, user.Age, user.Country, user.City, string.Empty).ToListAsync();
        }
    }
}
