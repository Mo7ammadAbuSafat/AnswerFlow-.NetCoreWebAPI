using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.ReportServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.ReportServices.Implementations
{
    public class QuestionReportServices : IQuestionReportServices
    {

        private readonly IQuestionReportRepository questionReportRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public QuestionReportServices(
            IQuestionReportRepository questionReportRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices)
        {
            this.questionReportRepository = questionReportRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync()
        {
            var reports = await questionReportRepository.GetQuestionReportsAsync();
            return mapper.Map<IEnumerable<QuestionReportResponseDto>>(reports);
        }

        public async Task ReportQuestionAsync(QuestionReportRequestDto questionReportRequestDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(questionReportRequestDto.UserId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionReportRequestDto.QuestionId);
            var questionReport = new QuestionReport()
            {
                CreationDate = DateTime.Now,
                User = user,
                Description = questionReportRequestDto.Description,
            };
            question.Reports.Add(questionReport);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CloseQuestionReportAsync(int reportId)
        {
            var report = await basedRepositoryServices.GetNonNullQuesitonReportByIdAsync(reportId);
            if (report.Status == ReportStatus.Closed)
            {
                throw new BadRequestException(ReportExceptionMessages.AlreadyClosed);
            }
            report.Status = ReportStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }


    }
}
