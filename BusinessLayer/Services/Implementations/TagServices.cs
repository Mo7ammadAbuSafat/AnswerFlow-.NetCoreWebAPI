using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.Services.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementations
{
    public class TagServices : ITagServices
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;

        public TagServices(ITagRepository tagRepository, IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<TagResponseDto>> GetAllTagsAsync()
        {
            var tags = await tagRepository.GetAllTagsAsync();
            return mapper.Map<IEnumerable<TagResponseDto>>(tags);
        }
    }
}
