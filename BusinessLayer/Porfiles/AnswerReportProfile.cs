using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
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
