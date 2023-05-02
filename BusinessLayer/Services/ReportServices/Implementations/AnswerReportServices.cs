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
    public class AnswerReportServices : IAnswerReportServices
    {
        private readonly IAnswerReportRepository answerReportRepository;
        private readonly IUserRepository userRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;


        public AnswerReportServices(IAnswerReportRepository answerReportRepository, IUserRepository userRepository, IAnswerRepository answerRepository, IMapper mapper, IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.answerReportRepository = answerReportRepository;
            this.userRepository = userRepository;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<IEnumerable<AnswerReportResponseDto>> GetAnswerReportsAsync()
        {
            var reports = await answerReportRepository.GetAnswerReportsAsync();
            return mapper.Map<IEnumerable<AnswerReportResponseDto>>(reports);
        }

        public async Task ReportAnswerAsync(AnswerReportRequestDto answerReportRequestDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(answerReportRequestDto.UserId);
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
