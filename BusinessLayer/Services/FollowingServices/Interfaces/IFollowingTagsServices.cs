using BusinessLayer.DTOs.TagDtos;

namespace BusinessLayer.Services.FollowingServices.Interfaces
{
    public interface IFollowingTagsServices
    {
        Task FollowTagAsync(int userId, int tagId);
        Task<IEnumerable<TagResponseDto>> GetFollowingTagsForUserByIdAsync(int userId);
        Task UnfollowTagAsync(int userId, int tagId);
    }
}