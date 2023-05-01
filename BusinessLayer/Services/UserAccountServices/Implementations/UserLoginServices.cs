using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.GeneralServices;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserLoginServices : IUserLoginServices
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserLoginServices(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<UserOverviewResponseDto> LoginUserAsync(UserLoginRequestDto userLogin)
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
            return mapper.Map<UserOverviewResponseDto>(user);
        }
    }
}
