using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserServicesFacade
    {
        Task BlockUserFromPostingAsync(int userId);
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
        Task<FullUserResponseDto> GetFullUserByIdAsync(int userId);
        Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId);
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync();
        Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync();
        Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId);
        Task<UserOverviewResponseDto> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task ResendVerificationCodeAsync(int userId);
        Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto);
        Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email);
        Task UnblockUserFromPostingAsync(int userId);
        Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
        Task UpgradeUserToAdminAsync(int userId);
        Task UpgradeUserToExpertAsync(int userId);
        Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code);
        Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
           int pageNumber,
           int pageSize,
           int userId);
    }
}