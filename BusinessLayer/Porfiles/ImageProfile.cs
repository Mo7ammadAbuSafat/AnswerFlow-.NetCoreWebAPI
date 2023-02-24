using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            CreateMap<Image, ImageDto>();
        }
    }
}
