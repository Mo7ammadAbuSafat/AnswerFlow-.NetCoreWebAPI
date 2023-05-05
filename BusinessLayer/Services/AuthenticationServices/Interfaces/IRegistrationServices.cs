using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface IRegistrationServices
    {
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(int userId);
        Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code);
    }
}