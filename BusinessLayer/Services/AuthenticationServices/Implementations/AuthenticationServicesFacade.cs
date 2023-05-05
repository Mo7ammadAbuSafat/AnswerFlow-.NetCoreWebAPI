using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class AuthenticationServicesFacade : IAuthenticationServicesFacade
    {
        private readonly ILoginServices userLoginServices;
        private readonly IUserPasswordServices userPasswordServices;
        private readonly IRegistrationServices userRegistrationServices;

        public AuthenticationServicesFacade(
            ILoginServices userLoginServices,
            IUserPasswordServices userPasswordServices,
            IRegistrationServices userRegistrationServices
            )
        {
            this.userLoginServices = userLoginServices;
            this.userPasswordServices = userPasswordServices;
            this.userRegistrationServices = userRegistrationServices;
        }
        public async Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {
            return await userRegistrationServices.RegisterUserAsync(userRegistration);
        }

        public async Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code)
        {
            return await userRegistrationServices.VerifyEmailAsync(userId, code);
        }

        public async Task ResendVerificationCodeAsync(int userId)
        {
            await userRegistrationServices.ResendVerificationCodeAsync(userId);
        }

        public async Task<string> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            return await userLoginServices.LoginUserAsync(userLogin);
        }

        public async Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email)
        {
            return await userPasswordServices.SendResetPasswordCodeAsync(email);
        }

        public async Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            await userPasswordServices.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordDto);
        }
    }
}
