using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface IAuthenticationServicesFacade
    {
        Task<string> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(int userId);
        Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto);
        Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email);
        Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code);
    }
}