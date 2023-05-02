using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.TagServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.TagServices.Implementations
{
    public class TagServices : ITagServices
    {
        private readonly ITagRepository tagRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public TagServices(ITagRepository tagRepository, IMapper mapper, IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.tagRepository = tagRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
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
                throw new BadRequestException(TagExceptionMessages.AlreadyTagWithThisName);
            }
            var tag = mapper.Map<Tag>(tagRequestDto);
            await tagRepository.AddAsync(tag);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<TagResponseDto>(tag);
        }

        public async Task<TagResponseDto> UpdateTagAsync(int tagId, TagRequestDto tagRequestDto)
        {
            var tag = await basedRepositoryServices.GetNonNullTagByIdAsync(tagId);
            var tagWithNewName = await tagRepository.GetTagByNameAsync(tagRequestDto.Name);
            if (tagWithNewName != null && tagWithNewName.Id != tag.Id)
            {
                throw new BadRequestException(TagExceptionMessages.AlreadyTagWithThisName);
            }
            tag.Name = tagRequestDto.Name.ToLower();
            tag.SourceLink = tagRequestDto.SourceLink;
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<TagResponseDto>(tag);
        }

        public async Task DeleteTagAsync(int tagId)
        {
            var tag = await basedRepositoryServices.GetNonNullTagByIdAsync(tagId);

            tagRepository.Delete(tag);
            await unitOfWork.SaveChangesAsync();
        }

    }
}
