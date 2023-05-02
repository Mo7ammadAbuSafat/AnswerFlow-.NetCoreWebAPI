using AutoMapper;
using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AnswerServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.AnswerServices.Implementations
{
    public class AnswerServices : IAnswerServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAnswerRepository answerRepository;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;

        public AnswerServices(IUnitOfWork unitOfWork, IAnswerRepository answerRepository, IBasedRepositoryServices basedRepositoryServices, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<AnswerResponseDto>> GetAnswersForQuestionAsync(int questionId)
        {
            var answers = await answerRepository.GetAnswersForQuestionAsync(questionId);
            return mapper.Map<IEnumerable<AnswerResponseDto>>(answers);
        }

        public async Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerToAddRequestDto answerToAddRequestDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(answerToAddRequestDto.UserId);
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException(UserExceptionMessages.BlocedUserFromPosting);
            }
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = new Answer()
            {
                UserId = user.Id,
                Body = answerToAddRequestDto.Body,
                CreationDate = DateTime.Now,
            };
            question.Answers.Add(answer);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task DeleteAnswerAsync(int questionId, int answerId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            answerRepository.Delete(answer);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerUpdateRequestDto answerUpdateRequestDto)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            answer.Body = answerUpdateRequestDto.Body;

            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task ApproveAnswerAsync(int questionId, int answerId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }

            answer.AnswerStatus = AnswerStatus.Approved;
            question.Status = QuestionStatus.Closed;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
