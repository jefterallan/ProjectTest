using ProjectTest.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectTest.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<string>> StoreData(string[] users = null);
        List<TblUser> ReadTextFile(string[] users = null);
        TblUser Validate(string[] splitData, int linesCount);
        bool ValidateCredentials(string username, string password);
    }
}
