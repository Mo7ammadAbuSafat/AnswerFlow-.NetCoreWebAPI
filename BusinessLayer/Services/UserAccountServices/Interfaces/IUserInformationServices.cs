using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserInformationServices
    {
        Task<FullUserResponseDto> GetFullUserByIdAsync(int userId);
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync();
        Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
    }
}