using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Commons;
using ProjectTest.Data;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories;
using ProjectTest.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ProjectTest.Test.Repositories
{
    public class UserRepositoryTest
    {
        public TblUser User { get; set; }
        public UserRepository UserRepository { get; set; }
        public ProjectTestContext Context { get; set; }

        #region System Constants
        private const string NAME = "ELLIE";
        private const string GENDER = "FEMALE";
        private const string AGE = "1";
        private const string DIFFERENTAGE = "2";
        private const string COUNTRY = "US";
        private const string CITY = "AUSTIN";
        #endregion

        public UserRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProjectTestContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

            Context = new ProjectTestContext(options);

            UserRepository = new UserRepository(Context);

            User = UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY);
        }

        [Fact]
        public async Task ShouldStoreData()
        {
            await UserRepository.StoreData(User);

            Context.TblUsers.FirstOrDefault().Should().Be(User);
        }

        [Fact]
        public async Task ShouldNotStoreNullData()
        {
            TblUser nullUser = null;

            var result = await Record.ExceptionAsync(() => UserRepository.StoreData(nullUser));

            result.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public async Task ShouldFindDuplicatedData()
        {
            await UserRepository.StoreData(User);

            var result = await UserRepository.FindDuplicatedData(User);

            result.Should().Be(User);
        }

        [Fact]
        public async Task ShouldNotFindDuplicatedData()
        {
            await UserRepository.StoreData(UsersDto.NewUser(NAME, GENDER, DIFFERENTAGE, COUNTRY, CITY));

            var result = await UserRepository.FindDuplicatedData(User);

            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldGetFullDataFromUsersTable()
        {
            var listOfUsers = new List<TblUser>() { UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY), UsersDto.NewUser(NAME, GENDER, DIFFERENTAGE, COUNTRY, CITY) };

            listOfUsers.ForEach(x => x.Id = Guid.NewGuid());

            await GenericFunctions.ForEachAsync(listOfUsers, UserRepository.StoreData);

            var result = await UserRepository.GetData();

            result.Should().BeEquivalentTo(listOfUsers.AsEnumerable());
        }
    }
}
