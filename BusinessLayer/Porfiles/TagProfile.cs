using AutoMapper;
using BusinessLayer.DTOs.TagDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Profiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagResponseDto>();
            CreateMap<TagRequestDto, Tag>();
        }
    }
}
