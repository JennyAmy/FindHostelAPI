using HousingAPI.Dtos.UserDto;
using HousingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HousingAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);

        void Register(RegisterDto registerDto);

        Task<bool> UserAlreadyExists(string userName);
    }
}
