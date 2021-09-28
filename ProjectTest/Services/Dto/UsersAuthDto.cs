using ProjectTest.Commons;
using ProjectTest.Data.Models;
using System;

namespace ProjectTest.Services.Dto
{
    public class UsersAuthDto
    {
        public string User { get; set; }
        public string Password { get; set; }

        public UsersAuthDto() { }

        public UsersAuthDto(string user, string password)
        {
            User = user;
            Password = password;
        }

        public static TblUsersAuth NewUser(string user, string password)
        {            
            return new TblUsersAuth()
            {
                User = ValidateUser(user),
                Password =  GenericFunctions.EncodingPassword(password)
            };
        }

        public static string ValidateUser(string user)
        {
            if (GenericFunctions.ValidateUser(user).Success)
                return user;
            else
                throw new AccessViolationException("Invalid User informed!");
        }
    }
}
