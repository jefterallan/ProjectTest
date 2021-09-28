using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using ProjectTest.Commons;
using ProjectTest.Services;
using ProjectTest.Services.Dto;
using System;
using Xunit;

namespace ProjectTest.Test.Services
{
    public class UserServiceTest
    {
        private readonly AutoMocker AutoMocker;
        private readonly Mock<IOptions<AppSettingsMap>> AppSettings;

        #region System Constants
        private const int LINESCOUNT = 1;
        private const string NAME = "ELLIE";
        private const string GENDER = "FEMALE";
        private const string AGE = "1";
        private const string COUNTRY = "US";
        private const string CITY = "AUSTIN";
        private const string FILEPATH = "C:/Users/Jefter_Costa/Documents/";
        private const string FILENAME = "people_data.txt";
        private const string DELIMITER = ",";
        #endregion

        public UserServiceTest()
        {
            AutoMocker = new AutoMocker();
            AppSettings = AutoMocker.GetMock<IOptions<AppSettingsMap>>();

            AppSettings.Setup(x => x.Value).Returns(new AppSettingsMap() { FilePath = FILEPATH, FileName = FILENAME, Delimiter = DELIMITER });
        }

        [Fact]
        public void ShouldValidateCorrectLenghtOfArray()
        {
            var service = AutoMocker.CreateInstance<UserService>();

            string[] validArrayLenght = { NAME, GENDER, AGE, COUNTRY, CITY };

            var result = Record.Exception(() => service.Validate(validArrayLenght, LINESCOUNT));

            result.Should().BeNull();
        }

        [Fact]
        public void ShouldValidateIncorrectLenghtOfArray()
        {
            var service = AutoMocker.CreateInstance<UserService>();
            string[] invalidArrayLenght = { NAME, GENDER, AGE, COUNTRY };

            var result = Record.Exception(() => service.Validate(invalidArrayLenght, LINESCOUNT));

            result.Should().BeOfType<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ShouldHaveValidContentOfCamps()
        {
            var service = AutoMocker.CreateInstance<UserService>();
            string[] validContentOfArray = { NAME, GENDER, AGE, COUNTRY, CITY };

            var result = Record.Exception(() => service.Validate(validContentOfArray, LINESCOUNT));

            result.Should().BeNull();
        }

        [Fact]
        public void ShouldHaveInvalidContentOfCamps()
        {
            var service = AutoMocker.CreateInstance<UserService>();
            string[] invalidContentOfArray = { NAME, string.Empty, AGE, COUNTRY, string.Empty };

            var result = Record.Exception(() => service.Validate(invalidContentOfArray, LINESCOUNT));

            result.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void ShouldReturnAValidatedUser()
        {
            var service = AutoMocker.CreateInstance<UserService>();
            string[] validContentOfArray = { NAME, GENDER, AGE, COUNTRY, CITY };

            var result = service.Validate(validContentOfArray, LINESCOUNT);

            result.Should().BeEquivalentTo(UsersDto.NewUser(NAME, GENDER, AGE, COUNTRY, CITY));
        }
    }
}
