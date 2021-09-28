using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using ProjectTest.Commons;
using ProjectTest.Data;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories;
using ProjectTest.Data.Repositories.Interfaces;
using ProjectTest.Services;
using ProjectTest.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTest.Test.Repositories
{
    public class SpStoreUserDataRepositoryTest
    {
        public TblUser User { get; set; }
        public SpStoreUserDataRepository SpStoreUserDataRepository { get; set; }
        public ProjectTestContext Context { get; set; }

        private readonly AutoMocker AutoMocker;

        private readonly Mock<IOptions<AppSettingsMap>> AppSettings;

        #region System Constants
        private const string NAME = "ELLIE";
        private const string GENDER = "FEMALE";
        private const string AGE = "1";
        private const string COUNTRY = "US";
        private const string CITY = "AUSTIN";
        private const string FILEPATH = "C:/Users/Jefter_Costa/Documents/";
        private const string FILENAME = "people_data.txt";
        private const string DELIMITER = ",";
        #endregion

        public SpStoreUserDataRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProjectTestContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

            Context = new ProjectTestContext(options);

            SpStoreUserDataRepository = new SpStoreUserDataRepository(Context);

            User = UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY);

            AutoMocker = new AutoMocker(); AppSettings = AutoMocker.GetMock<IOptions<AppSettingsMap>>();

            AppSettings.Setup(x => x.Value).Returns(new AppSettingsMap() { FilePath = FILEPATH, FileName = FILENAME, Delimiter = DELIMITER });
        }

        [Fact]
        public void ShouldPersistData()
        {
            var parametersMock = new string[] { string.Format("{0},{1},{2},{3},{4}", NAME, GENDER, AGE, COUNTRY, CITY) };
            var StoreUserDataMock = new SpStoreUserData() { Result = "The data was Stored with success!" };
            var repositoryResultMock = new List<SpStoreUserData>() { StoreUserDataMock };
            var serviceResultMock = new List<string>() { "The data was Stored with success!" };

            var service = AutoMocker.CreateInstance<UserService>();
            var repository = AutoMocker.GetMock<ISpStoreUserDataRepository>();

            repository.Setup(r => r.StoreData(It.IsAny<TblUser>())).Returns(Task.FromResult(repositoryResultMock));

            var result = service.StoreData(parametersMock).Result;

            repository.Verify(r => r.StoreData(It.IsAny<TblUser>()), Times.Once);
            result.Should().NotBeNull().And.BeEquivalentTo(serviceResultMock);
        }

        [Fact]
        public async Task ShouldGotErrorToPersistNullUser()
        {
            TblUser nullUser = null;

            var result = await Record.ExceptionAsync(() => SpStoreUserDataRepository.StoreData(nullUser));

            result.Should().BeOfType<NullReferenceException>();
        }
    }
}
