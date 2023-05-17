using BusinessLayer.DTOs.AuthenticationDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class UserPasswordServices : IUserPasswordServices
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public UserPasswordServices(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task SendResetPasswordCodeAsync(string email)
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
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByEmailAsync(resetPasswordDto.Email);
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
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
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
