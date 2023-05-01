using BusinessLayer.DTOs.TagDtos;

namespace BusinessLayer.Services.TagServices.Interfaces
{
    public interface ITagServices
    {
        Task<IEnumerable<TagResponseDto>> GetAllTagsAsync();
        Task<TagResponseDto> AddNewTagAsync(TagRequestDto tagRequestDto);
        Task DeleteTagAsync(int tagId);
        Task<TagResponseDto> UpdateTagAsync(int tagId, TagRequestDto tagRequestDto);
    }
}