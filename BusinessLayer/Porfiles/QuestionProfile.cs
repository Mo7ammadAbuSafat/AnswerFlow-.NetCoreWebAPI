using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionResponseDto>();
            CreateMap<QuestionHistory, QuestionHistoryResponseDto>();
        }
    }
}
