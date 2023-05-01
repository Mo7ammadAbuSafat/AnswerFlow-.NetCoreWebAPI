using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.Services.ReportServices.Interfaces;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.ReportServices.Implementations
{
    public class DualReportDataServices : IDualReportDataServices
    {
        private readonly IQuestionReportRepository questionReportRepository;
        private readonly IAnswerReportRepository answerReportRepository;
        private readonly IMapper mapper;

        public DualReportDataServices(IQuestionReportRepository questionReportRepository, IAnswerReportRepository answerReportRepository, IMapper mapper)
        {
            this.questionReportRepository = questionReportRepository;
            this.answerReportRepository = answerReportRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ReportResponseDto>> GetReportsAsync()
        {
            var questionReports = await questionReportRepository.GetQuestionReportsAsync();
            var answerReports = await answerReportRepository.GetAnswerReportsAsync();
            var finalReports = mapper.Map<IEnumerable<ReportResponseDto>>(questionReports)
                        .Union(mapper.Map<IEnumerable<ReportResponseDto>>(answerReports))
                        .OrderByDescending(x => x.CreationDate)
                        .ToList();
            return finalReports;
        }
    }
}
