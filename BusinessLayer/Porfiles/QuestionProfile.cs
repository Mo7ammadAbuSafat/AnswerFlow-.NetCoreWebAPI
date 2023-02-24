using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionOverviewDto>();
            CreateMap<Question, QuestionDto>();
        }
    }
}
