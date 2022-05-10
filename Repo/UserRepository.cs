using HousingAPI.DbContexts;
using HousingAPI.Dtos.UserDto;
using HousingAPI.Interfaces;
using HousingAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace HousingAPI.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == userName);

            if (user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;

            return user;
            
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {

            using (var hmac = new HMACSHA512(passwordKey))
            {
               var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for(int i=0;  i<passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(RegisterDto registerDto)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDto.Password));
            }

            User user = new User();
            user.Username = registerDto.Username;
            user.Email = registerDto.Email;
            user.Phone = registerDto.Phone;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            context.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName)
        {
            return await context.Users.AnyAsync(x => x.Username == userName);
        }
    }
}
