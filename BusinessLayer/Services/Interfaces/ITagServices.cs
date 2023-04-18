using BusinessLayer.DTOs.TagDtos;

namespace BusinessLayer.Services.Interfaces
{
    public interface ITagServices
    {
        Task<IEnumerable<TagResponseDto>> GetAllTagsAsync();
        Task<TagResponseDto> AddNewTagAsync(TagRequestDto tagRequestDto);
        Task<TagResponseDto> UpdateTagAsync(int tagId, TagRequestDto tagRequestDto);
        Task DeleteTagAsync(int tagId);
    }
}