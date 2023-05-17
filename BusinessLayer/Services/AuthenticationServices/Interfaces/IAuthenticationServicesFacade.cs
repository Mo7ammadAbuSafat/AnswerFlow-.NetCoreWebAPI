using BusinessLayer.DTOs.AuthenticationDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface IAuthenticationServicesFacade
    {
        Task<string> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserOverviewResponseDto> GetUserByJwtTokenAsync();
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(string email);
        Task ResetPasswordAsync(ResetPasswordRequestDto resetPasswordDto);
        Task SendResetPasswordCodeAsync(string email);
        Task<string> VerifyEmailAsync(string email, string code);
    }
}