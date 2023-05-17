using AutoMapper;
using BusinessLayer.DTOs.ReportDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
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
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public QuestionReportServices(
            IQuestionReportRepository questionReportRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.questionReportRepository = questionReportRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<IEnumerable<QuestionReportResponseDto>> GetQuestionReportsAsync()
        {
            var reports = await questionReportRepository.GetQuestionReportsAsync();
            return mapper.Map<IEnumerable<QuestionReportResponseDto>>(reports);
        }

        public async Task ReportQuestionAsync(QuestionReportRequestDto questionReportRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserId();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
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
