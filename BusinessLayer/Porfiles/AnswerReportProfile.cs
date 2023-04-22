using AutoMapper;
using BusinessLayer.DTOs.QuestionReportDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class AnswerReportProfile : Profile
    {
        public AnswerReportProfile()
        {
            CreateMap<AnswerReport, AnswerReportResponseDto>();
        }
    }
}
