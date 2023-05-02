using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserLoginServices
    {
        Task<UserOverviewResponseDto> LoginUserAsync(UserLoginRequestDto userLogin);
    }
}