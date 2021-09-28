using Microsoft.Extensions.Options;
using ProjectTest.Commons;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories.Interfaces;
using ProjectTest.Services.Dto;
using ProjectTest.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.Services
{
    public class UserService : IUserService
    {
        private readonly ISpStoreUserDataRepository SpStoreUserDataRepository;
        private readonly IUsersAuthRepository UserAuthRepository;
        private readonly string FileLocation;
        private readonly string Delimiter;

        #region System Contants
        private const int ARRAYLENGHT = 5;
        private const int NAME = 0;
        private const int GENDER = 1;
        private const int AGE = 2;
        private const int COUNTRY = 3;
        private const int CITY = 4;
        #endregion

        public UserService(IOptions<AppSettingsMap> appSettings,
            ISpStoreUserDataRepository spStoreUserDataRepository,
            IUsersAuthRepository userAuthRepository)
        {
            SpStoreUserDataRepository = spStoreUserDataRepository;
            UserAuthRepository = userAuthRepository;

            FileLocation = appSettings.Value.FilePath + appSettings.Value.FileName;
            Delimiter = appSettings.Value.Delimiter;
        }

        public async Task<List<string>> StoreData(string[] users = null)
        {
            var results = new List<string>();

            foreach (var item in ReadTextFile(users))
            {
                var spModelResult = await SpStoreUserDataRepository.StoreData(item);

                var sp = spModelResult.FirstOrDefault();

                if (sp != null)
                    results.Add(sp.Result);
            }

            return results;
        }

        public List<TblUser> ReadTextFile(string[] users = null)
        {
            var listOfUsers = new List<TblUser>();
            string[] lines;
                        
            lines = users ?? System.IO.File.ReadAllLines(@FileLocation);
            
            int linesCount = 1;

            foreach (string line in lines)
            {
                var user = Validate(line.Split(Delimiter), linesCount);                               

                if (user != null)
                    listOfUsers.Add(user);
               
                linesCount++;
            }

            return listOfUsers;
        }

        public TblUser Validate(string[] splitData, int linesCount)
        {
            if (splitData.Length != ARRAYLENGHT)
                throw new ArgumentOutOfRangeException(string.Format("The data has a incorrect number of parameters for line {0}.", linesCount));

            foreach (var item in splitData)
                if (string.IsNullOrWhiteSpace(item))
                    throw new ArgumentNullException(string.Format("The data has nullable parameter for line {0}. All the parameters is required!", linesCount));

            return UsersDto.NewUser(splitData[NAME], splitData[GENDER], splitData[AGE], splitData[COUNTRY], splitData[CITY]);
        }

        public bool ValidateCredentials(string username, string password)
        {//(admin) 0DPiKuNIrrVmD8IUCuw1hQxNqZc=
            return UserAuthRepository.ValidateCredentials(UsersAuthDto.NewUser(username, password));
        }
    }
}
