using AutoMapper;
using BusinessLayer.DTOs.AnswerDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerResponseDto>();
        }
    }
}
