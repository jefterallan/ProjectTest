using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProjectTest.Data;
using ProjectTest.Data.Models;
using ProjectTest.Data.Repositories;
using ProjectTest.Services.Dto;
using System;
using Xunit;


namespace ProjectTest.Test.Repositories
{
    public class UsersAuthRepositoryTest
    {
        public TblUsersAuth UsersAuth { get; set; }
        public UsersAuthRepository UserAuthRepository { get; set; }
        public ProjectTestContext Context { get; set; }

        #region System Constants
        private const string USERNAME = "ADMIN";
        private const string PASSWORD = "TEST";
        private const string PASSWORDTWO = "TESTTWO";
        #endregion

        public UsersAuthRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ProjectTestContext>()
                               .UseInMemoryDatabase(Guid.NewGuid().ToString())
                               .Options;

            Context = new ProjectTestContext(options);

            UserAuthRepository = new UsersAuthRepository(Context);

            UsersAuth = UsersAuthDto.NewUser(USERNAME, PASSWORD);
        }

        [Fact]
        public void ShouldFindCredentials()
        {
            Context.TblUsersAuths.Add(UsersAuth);
            Context.SaveChanges();

            var result = UserAuthRepository.ValidateCredentials(UsersAuth);

            result.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotFindCredentials()
        {
            Context.TblUsersAuths.Add(UsersAuth);
            Context.SaveChanges();

            var result = UserAuthRepository.ValidateCredentials(UsersAuthDto.NewUser(USERNAME, PASSWORDTWO));

            result.Should().BeFalse();
        }
    }
}
