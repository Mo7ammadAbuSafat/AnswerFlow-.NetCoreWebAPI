using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementations
{
    public class TagServices : ITagServices
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public TagServices(ITagRepository tagRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TagResponseDto>> GetAllTagsAsync()
        {
            var tags = await tagRepository.GetAllTagsAsync();
            return mapper.Map<IEnumerable<TagResponseDto>>(tags);
        }

        public async Task<TagResponseDto> AddNewTagAsync(TagRequestDto tagRequestDto)
        {
            tagRequestDto.Name = tagRequestDto.Name.ToLower();

            var existTag = await tagRepository.GetTagByNameAsync(tagRequestDto.Name);
            if (existTag != null)
            {
                throw new BadRequestException("tag already exist");
            }
            var tag = mapper.Map<Tag>(tagRequestDto);
            await tagRepository.AddAsync(tag);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<TagResponseDto>(tag);
        }

        public async Task<TagResponseDto> UpdateTagAsync(int tagId, TagRequestDto tagRequestDto)
        {
            var tag = await tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                throw new BadRequestException("tag is not exist");
            }

            tag.Name = tagRequestDto.Name.ToLower();
            tag.Description = tagRequestDto.Description;
            tag.SourceLink = tagRequestDto.SourceLink;

            await unitOfWork.SaveChangesAsync();
            return mapper.Map<TagResponseDto>(tag);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await tagRepository.GetTagByIdAsync(tagId);
            if (tag == null)
            {
                throw new BadRequestException("tag is not exist");
            }

            tagRepository.Delete(tag);
            await unitOfWork.SaveChangesAsync();
        }


    }
}
