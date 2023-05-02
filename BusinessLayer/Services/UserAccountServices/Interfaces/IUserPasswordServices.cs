using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserPasswordServices
    {
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
        Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto changePasswordDto);
        Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email);
    }
}