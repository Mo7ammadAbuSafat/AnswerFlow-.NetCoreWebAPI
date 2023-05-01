using AutoMapper;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserPasswordServices : IUserPasswordServices
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public UserPasswordServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email)
        {
            var user = await userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new NotFoundException(UserExceptionMessages.NotFoundUserByEmail);
            }
            user.ResetPasswordCode = RandomCodeGenerator.GenerateRandomCode();
            user.ResetPasswordCodeExpiresDate = DateTime.Now.AddDays(1);
            await unitOfWork.SaveChangesAsync();
            var emailMessage = EmailMessageGenerator.GenerateEmailMessageForResetPasswordCode(user.ResetPasswordCode);
            EmailSender.SendEmailWithCode(email, emailMessage);
            return mapper.Map<UserOverviewResponseDto>(user);
        }

        public async Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.ResetPasswordCode != resetPasswordDto.Code)
            {
                throw new BadRequestException(UserExceptionMessages.InvalidCode);
            }
            if (user.ResetPasswordCodeExpiresDate < DateTime.Now)
            {
                await SendResetPasswordCodeAsync(user.Email);
                throw new BadRequestException(UserExceptionMessages.ExpiredCode);
            }
            PasswordHasher.CreatePasswordHash(resetPasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.ResetPasswordCode = null;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (!PasswordHasher.VerifyPassword(changePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                throw new BadRequestException(UserExceptionMessages.IncorrectPassword);
            }
            PasswordHasher.CreatePasswordHash(changePasswordDto.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
