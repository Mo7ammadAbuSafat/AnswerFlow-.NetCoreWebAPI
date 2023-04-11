using BusinessLayer.DTOs.TagDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITagServices
    {
        Task<IEnumerable<TagResponseDto>> GetAllTagsAsync();
    }
}