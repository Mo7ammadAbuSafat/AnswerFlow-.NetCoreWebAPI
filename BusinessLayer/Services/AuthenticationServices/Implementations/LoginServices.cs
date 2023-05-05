using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using Microsoft.IdentityModel.Tokens;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class LoginServices : ILoginServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginServices(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<string> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            var user = await userRepository.GetUserByEmail(userLogin.Email);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserByEmail);
            }
            if (!PasswordHasher.VerifyPassword(userLogin.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
            }
            if (user.VerifiedDate == null)
            {
                throw new BadRequestException(UserExceptionMessages.MustVerifyEmail);
            }
            var token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Type.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                Configration.config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
