using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.AuthenticationServices.Interfaces
{
    public interface ILoginServices
    {
        Task<string> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserOverviewResponseDto> GetUserByJwtTokenAsync();
    }
}