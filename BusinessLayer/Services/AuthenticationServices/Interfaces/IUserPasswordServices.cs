using BusinessLayer.DTOs.AuthenticationDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface IUserPasswordServices
    {
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
        Task ResetPasswordAsync(ResetPasswordRequestDto changePasswordDto);
        Task SendResetPasswordCodeAsync(string email);
    }
}