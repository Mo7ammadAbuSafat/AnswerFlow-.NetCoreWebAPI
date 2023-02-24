using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, TagDto>();
        }
    }
}
