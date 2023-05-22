using BusinessLayer.DTOs.AuthenticationDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;

namespace BusinessLayer.Services.AuthenticationServices.Implementations
{
    public class AuthenticationServicesFacade : IAuthenticationServicesFacade
    {
        private readonly ILoginServices loginServices;
        private readonly IUserPasswordServices userPasswordServices;
        private readonly IRegistrationServices registrationServices;

        public AuthenticationServicesFacade(
            ILoginServices loginServices,
            IUserPasswordServices userPasswordServices,
            IRegistrationServices registrationServices
            )
        {
            this.loginServices = loginServices;
            this.userPasswordServices = userPasswordServices;
            this.registrationServices = registrationServices;
        }
        public async Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {
            return await registrationServices.RegisterUserAsync(userRegistration);
        }

        public async Task<string> VerifyEmailAsync(string email, string code)
        {
            return await registrationServices.VerifyEmailAsync(email, code);
        }

        public async Task ResendVerificationCodeAsync(string email)
        {
            await registrationServices.ResendVerificationCodeAsync(email);
        }

        public async Task<string> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            return await loginServices.LoginUserAsync(userLogin);
        }

        public async Task<UserOverviewResponseDto> GetUserByJwtTokenAsync()
        {
            return await loginServices.GetUserByJwtTokenAsync();
        }

        public async Task SendResetPasswordCodeAsync(string email)
        {
            await userPasswordServices.SendResetPasswordCodeAsync(email);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordDto)
        {
            await userPasswordServices.ResetPasswordAsync(resetPasswordDto);
        }
    }
}
