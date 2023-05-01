using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserRegistrationServices
    {
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(int userId);
        Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code);
    }
}