using FluentAssertions;
using ProjectTest.Services.Dto;
using System;
using Xunit;

namespace ProjectTest.Test.Models
{
    public class UsersAuthModelTest
    {
        #region System Constants
        private const string USERNAME = "admin";
        private const string PASSWORD = "test";
        private const string INVALIDUSERNAME = "11=11";
        #endregion

        [Fact]
        public void ShouldHaveAValidUsername()
        {
            var result = Record.Exception(() => UsersAuthDto.NewUser(USERNAME, PASSWORD));

            result.Should().BeNull();
        }

        [Fact]
        public void ShouldHaveAInvalidUsername()
        {
            var result = Record.Exception(() => UsersAuthDto.NewUser(INVALIDUSERNAME, PASSWORD));

            result.Should().BeOfType<AccessViolationException>();
        }
    }
}
