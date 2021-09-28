using ProjectTest.Data.Models;
using ProjectTest.Data.Models.Enum;
using System;

namespace ProjectTest.Services.Dto
{
    public class UsersDto
    {
        public string Name { get; set; }
        public int Gender { get; set; }
        public int Age { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public UsersDto() { }

        public UsersDto(string name, string gender, string age, string country, string city)
        {
            Name = name.ToUpper();
            Gender = ValidateGender(gender.ToUpper());
            Age = ValidateAgeAsNumber(age);
            Country = country.ToUpper();
            City = city.ToUpper();
        }

        public static int ValidateGender(string gender)
        {
            if (gender == Genders.MALE.ToString())
                return Convert.ToInt32(Genders.MALE);

            if (gender == Genders.FEMALE.ToString())
                return Convert.ToInt32(Genders.FEMALE);

            throw new ArgumentException("Invalid Gender informed.", gender);
        }

        public static int ValidateAgeAsNumber(string age)
        {
            var result = Int32.TryParse(age, out int convertedAge);

            if (!result)
                throw new FormatException("The age informed is not a number!");

            if (convertedAge < 0)
                throw new ArgumentOutOfRangeException("Age", "The age informed is invalid!");

            return convertedAge;
        }

        public static TblUser NewUser(string name, string gender, string age, string country, string city)
        {
            return new TblUser()
            {
                Name = name.ToUpper(),
                Gender = ValidateGender(gender.ToUpper()),
                Age = ValidateAgeAsNumber(age),
                Country = country.ToUpper(),
                City = city.ToUpper(),
            };            
        }
    }
}
