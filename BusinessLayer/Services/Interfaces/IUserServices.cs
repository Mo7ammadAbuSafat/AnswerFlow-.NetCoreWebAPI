using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface IUserServices
    {
        Task<bool> RegisterUserAsync(UserRegistrationRequestDto userRegistration);
        Task<bool> LoginUserAsync(UserLoginRequestDto userLogin);
        Task<bool> VerifyEmailAsync(string token);
        Task FollowUserAsync(int userId, int followedUserId);
        Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId);
        Task UnfollowUserAsync(int userId, int followedUserId);
        Task FollowTagAsync(int userId, int tagId);
        Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId);
        Task UnfollowTagAsync(int userId, int tagId);
    }
}