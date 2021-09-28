using FluentAssertions;
using ProjectTest.Services.Dto;
using System;
using Xunit;

namespace ProjectTest.Test.Models
{
    public class UserModelTest
    {
        #region System Constants
        private const string NAME = "ELLIE";
        private const string GENDER = "FEMALE";
        private const string INVALIDGENDER = "OTHER";
        private const string AGE = "1";
        private const string INVALIDAGE = "1a";
        private const string INVALIDRANGEAGE = "-1";
        private const string COUNTRY = "US";
        private const string CITY = "AUSTIN";
        #endregion

        [Fact]
        public void ShouldHaveCorrectGenderforUser()
        {
            var result = Record.Exception(() => UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY));

            result.Should().BeNull();
        }

        [Fact]
        public void ShouldHaveIncorrectGenderforUser()
        {
            var result = Record.Exception(() => UsersDto.NewUser(NAME, INVALIDGENDER, AGE, COUNTRY, CITY));

            result.Should().BeOfType<ArgumentException>();
        }

        [Fact]
        public void ShouldHaveValidAgeForuser()
        {
            var result = Record.Exception(() => UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY));

            result.Should().BeNull();
        }

        [Fact]
        public void ShouldHaveInvalidAgeForUser()
        {
            var result = Record.Exception(() => UsersDto.NewUser(NAME, GENDER, INVALIDAGE, COUNTRY, CITY));

            result.Should().BeOfType<FormatException>();
        }

        [Fact]
        public void ShouldHaveAgeMinusZeroForUser()
        {
            var result = Record.Exception(() => UsersDto.NewUser(NAME, GENDER, INVALIDRANGEAGE, COUNTRY, CITY));

            result.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}
