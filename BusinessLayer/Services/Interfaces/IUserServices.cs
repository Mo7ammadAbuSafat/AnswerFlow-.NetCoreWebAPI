using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task<UserOverviewResponseDto> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code);
        Task ResendVerificationCodeAsync(int userId);
        Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email);
        Task ResendResetPasswordCodeAsync(int userId);
        Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto);
        Task ResetPasswordByOldPasswordAsync(int userId, ResetPasswordWithOldPasswordRequestDto resetPasswordDto);
        Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
        Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync();
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task<FullUserResponseDto> GetFullUserByIdAsync(int userId);
        Task FollowUserAsync(int userId, int followedUserId);
        Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId);
        Task UnfollowUserAsync(int userId, int followedUserId);
        Task FollowTagAsync(int userId, int tagId);
        Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId);
        Task UnfollowTagAsync(int userId, int tagId);
        Task BlockUserFromPostingAsync(int userId);
        Task UnblockUserFromPostingAsync(int userId);
        Task UpgradeUserToExpertAsync(int userId);
        Task UpgradeUserToAdminAsync(int userId);
        Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync();
        Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId);
        Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId);
    }
}