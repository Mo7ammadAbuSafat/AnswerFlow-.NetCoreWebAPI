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
    public class AnswerReportServices : IAnswerReportServices
    {
        private readonly IAnswerReportRepository answerReportRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;


        public AnswerReportServices(
            IAnswerReportRepository answerReportRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.answerReportRepository = answerReportRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<IEnumerable<AnswerReportResponseDto>> GetAnswerReportsAsync()
        {
            var reports = await answerReportRepository.GetAnswerReportsAsync();
            return mapper.Map<IEnumerable<AnswerReportResponseDto>>(reports);
        }

        public async Task ReportAnswerAsync(AnswerReportRequestDto answerReportRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserId();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerReportRequestDto.AnswerId);
            var answerReport = new AnswerReport()
            {
                CreationDate = DateTime.Now,
                User = user,
                Description = answerReportRequestDto.Description,
            };
            answer.Reports.Add(answerReport);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task CloseAnswerReportAsync(int reportId)
        {
            var report = await basedRepositoryServices.GetNonNullAnswerReportByIdAsync(reportId);
            if (report.Status == ReportStatus.Closed)
            {
                throw new BadRequestException(ReportExceptionMessages.AlreadyClosed);
            }
            report.Status = ReportStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
