using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class QuestionReportProfile : Profile
    {
        public QuestionReportProfile()
        {
            CreateMap<QuestionReport, QuestionReportResponseDto>();
            CreateMap<QuestionReport, ReportResponseDto>();
        }
    }
}
