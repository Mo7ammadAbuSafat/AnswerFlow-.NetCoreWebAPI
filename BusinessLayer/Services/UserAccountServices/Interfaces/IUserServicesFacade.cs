using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserServicesFacade
    {
        Task BlockUserFromPostingAsync(int userId);
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
        Task<FullUserResponseDto> GetFullUserByIdAsync(int userId);
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync();
        Task UnblockUserFromPostingAsync(int userId);
        Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
        Task UpgradeUserToAdminAsync(int userId);
        Task UpgradeUserToExpertAsync(int userId);
        Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
           int pageNumber,
           int pageSize,
           int userId);
    }
}