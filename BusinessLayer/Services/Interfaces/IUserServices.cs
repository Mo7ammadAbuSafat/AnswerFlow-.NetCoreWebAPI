using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserInformationResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task<UserInformationResponseDto> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<UserInformationResponseDto> VerifyEmailAsync(int userId, string code);
        Task ResendVerificationCodeAsync(int userId);
        Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email);
        Task ResendResetPasswordCodeAsync(int userId);
        Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto);
        Task ResetPasswordByOldPasswordAsync(int userId, ResetPasswordWithOldPasswordRequestDto resetPasswordDto);
        Task<UserInformationResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task FollowUserAsync(int userId, int followedUserId);
        Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId);
        Task UnfollowUserAsync(int userId, int followedUserId);
        Task FollowTagAsync(int userId, int tagId);
        Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId);
        Task UnfollowTagAsync(int userId, int tagId);
    }
}