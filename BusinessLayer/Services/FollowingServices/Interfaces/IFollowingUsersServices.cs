using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.FollowingServices.Interfaces
{
    public interface IFollowingUsersServices
    {
        Task FollowUserAsync(int userId, int followedUserId);
        Task<IEnumerable<UserOverviewResponseDto>> GetFollowingUsersForUserByIdAsync(int userId);
        Task UnfollowUserAsync(int userId, int followedUserId);
    }
}