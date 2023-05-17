using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface IRegistrationServices
    {
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(string email);
        Task<string> VerifyEmailAsync(string email, string code);
    }
}