using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class RegistrationServices : IRegistrationServices
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public RegistrationServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {
            if (userRepository.CheckIfEmailExists(userRegistration.Email))
            {
                throw new NotFoundException(UserExceptionMessages.EmailAlreadyExsist);
            }
            PasswordHasher.CreatePasswordHash(userRegistration.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User()
            {
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreationDate = DateTime.Now,
                VerificationCode = RandomCodeGenerator.GenerateRandomCode()
            };
            var emailMessage = EmailMessageGenerator.GenerateEmailMessageForCodeVerification(user.VerificationCode);
            EmailSender.SendEmailWithCode(userRegistration.Email, emailMessage);
            await userRepository.AddAsync(user);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task<string> VerifyEmailAsync(string email, string code)
        {
            var user = await basedRepositoryServices.GetNonNullUserByEmailAsync(email);
            if (code != user.VerificationCode)
            {
                throw new BadRequestException(UserExceptionMessages.InvalidCode);
            }
            user.VerifiedDate = DateTime.Now;
            await unitOfWork.SaveChangesAsync();
            return TokenGenerator.CreateToken(user);
        }

        public async Task ResendVerificationCodeAsync(string email)
        {
            var user = await basedRepositoryServices.GetNonNullUserByEmailAsync(email);
            user.VerificationCode = RandomCodeGenerator.GenerateRandomCode();
            await unitOfWork.SaveChangesAsync();
            var emailMessage = EmailMessageGenerator.GenerateEmailMessageForCodeVerification(user.VerificationCode);
            EmailSender.SendEmailWithCode(user.Email, emailMessage);
        }
    }
}
