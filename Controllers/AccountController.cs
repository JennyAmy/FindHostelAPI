using HousingAPI.Dtos.UserDto;
using HousingAPI.Extentions;
using HousingAPI.Interfaces;
using HousingAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HousingAPI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AccountController(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await unitOfWork.UserRepository.Authenticate(loginRequestDto.Username, loginRequestDto.Password);

            if(user == null)
            {
                return Unauthorized("Invalid Username or Password");
            }

            var loginResponse = new LoginResponseDto();
            loginResponse.UserName = user.Username;
            loginResponse.Token = CreateJWT(user);

            return Ok(loginResponse);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if(registerDto.Username.IsEmpty() || registerDto.Password.IsEmpty())
            {
                return BadRequest("Username or password cannot be empty");
            }
            if (await unitOfWork.UserRepository.UserAlreadyExists(registerDto.Username))
                return BadRequest("User already exists, please try another username");

            unitOfWork.UserRepository.Register(registerDto);
            await unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        private string CreateJWT(User user)
        {
            var secretKey = configuration.GetSection("AppSettings:Key").Value; //Gotten from the AppSettings.json configuartion
            var key = new SymmetricSecurityKey(Encoding.UTF8
               .GetBytes(secretKey));

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
