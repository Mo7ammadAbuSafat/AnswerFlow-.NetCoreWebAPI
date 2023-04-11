using AutoMapper;
using BusinessLayer.DTOs.ImageDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageResponseDto>();
        }
    }
}
