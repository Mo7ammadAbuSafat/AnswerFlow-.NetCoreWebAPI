using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.Implementations
{
    public class ReportServices : IReportServices
    {

        private readonly IQuestionReportRepository questionReportRepository;
        private readonly IAnswerReportRepository answerReportRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ReportServices(
            IQuestionReportRepository questionReportRepository,
            IAnswerReportRepository answerReportRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            this.questionReportRepository = questionReportRepository;
            this.answerReportRepository = answerReportRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
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

        public async Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync()
        {
            var reports = await questionReportRepository.GetQuestionReportsAsync();
            return mapper.Map<IEnumerable<QuestionReportResponseDto>>(reports);
        }

        public async Task<IEnumerable<AnswerReportResponseDto>> GetAnswerReportsAsync()
        {
            var reports = await answerReportRepository.GetAnswerReportsAsync();
            return mapper.Map<IEnumerable<AnswerReportResponseDto>>(reports);
        }

        public async Task<ReportsStatisticsResponseDto> GetReportsStatisticsAsync()
        {
            var questionReports = await questionReportRepository.GetQuestionReportsAsync();
            var answerReports = await answerReportRepository.GetAnswerReportsAsync();
            var statistics = new ReportsStatisticsResponseDto()
            {
                Count = questionReports.Count() + answerReports.Count(),
                LastMonthReportsCount = questionReports.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count()
                                        + answerReports.Where(q => q.CreationDate >= DateTime.Now.AddMonths(-1)).Count(),
            };
            return statistics;
        }

        public async Task CloseAnswerReportAsync(int reportId)
        {
            var report = await answerReportRepository.GetAnswerReportByIdAsync(reportId);
            if (report == null)
            {
                throw new NotFoundException("No report with this id");
            }
            if (report.Status == ReportStatus.Closed)
            {
                throw new BadRequestException("the report is already closed");
            }
            report.Status = ReportStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CloseQuestionReportAsync(int reportId)
        {
            var report = await questionReportRepository.GetQuestionReportByIdAsync(reportId);
            if (report == null)
            {
                throw new NotFoundException("No report with this id");
            }
            if (report.Status == ReportStatus.Closed)
            {
                throw new BadRequestException("the report is already closed");
            }
            report.Status = ReportStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
