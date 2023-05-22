using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class LoginServices : ILoginServices
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthenticatedUserServices authenticatedUserServices;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;

        public LoginServices(
            IUserRepository userRepository,
            IBasedRepositoryServices basedRepositoryServices,
            IMapper mapper,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.userRepository = userRepository;
            this.basedRepositoryServices = basedRepositoryServices;
            this.mapper = mapper;
            this.authenticatedUserServices = authenticatedUserServices;
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
            var token = TokenGenerator.CreateToken(user);
            return token;
        }

        public async Task<UserOverviewResponseDto> GetUserByJwtTokenAsync()
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            return mapper.Map<UserOverviewResponseDto>(user);
        }

    }
}
