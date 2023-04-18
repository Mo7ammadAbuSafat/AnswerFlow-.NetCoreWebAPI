using AutoMapper;
using BusinessLayer.DTOs.QuestionReportDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class QuestionReportProfile : Profile
    {
        public QuestionReportProfile()
        {
            CreateMap<QuestionReport, QuestionReportResponseDto>();
        }
    }
}
