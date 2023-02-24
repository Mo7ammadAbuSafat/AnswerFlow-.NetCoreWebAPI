using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDto>();
        }
    }
}
