using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectTest.Commons
{
    public static class GenericFunctions
    {
        public static async Task ForEachTaskAsync<T>(this Task<List<T>> list, Func<T, Task> func)
        {
            foreach (var value in list.Result)
                await func(value);
        }

        public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
        {
            foreach (var value in list)
                await func(value);
        }

        public static Match ValidateUser(string user)
        {
            string regex = @"^[a-zA-Z][a-zA-Z0-9]{4,11}$";
            Regex regUser = new(regex);

            return regUser.Match(user);
        }

        public static string EncodingPassword(string password)
        {
            return Convert.ToBase64String(new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
