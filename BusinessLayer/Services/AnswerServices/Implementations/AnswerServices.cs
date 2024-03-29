﻿using AutoMapper;
using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AnswerServices.Interfaces;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.NotificationServices.Interfaces;
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
        private readonly IAuthenticatedUserServices authenticatedUserServices;
        private readonly INotificationServices notificationServices;

        public AnswerServices(
            IUnitOfWork unitOfWork,
            IAnswerRepository answerRepository,
            IBasedRepositoryServices basedRepositoryServices,
            IMapper mapper,
            IAuthenticatedUserServices authenticatedUserServices,
            INotificationServices notificationServices)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.answerRepository = answerRepository;
            this.mapper = mapper;
            this.authenticatedUserServices = authenticatedUserServices;
            this.notificationServices = notificationServices;
        }

        public async Task<IEnumerable<AnswerResponseDto>> GetAnswersForQuestionAsync(int questionId)
        {
            var answers = await answerRepository.GetAnswersForQuestionAsync(questionId);
            return mapper.Map<IEnumerable<AnswerResponseDto>>(answers);
        }

        public async Task<AnswerResponseDto> GetAnswerAsync(int questionId, int answerId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task<AnswerResponseDto> AddNewAnswerAsync(int questionId, AnswerRequestDto answerRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException(UserExceptionMessages.BlocedUserFromPosting);
            }
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = new Answer()
            {
                UserId = user.Id,
                Body = answerRequestDto.Body,
                CreationDate = DateTime.Now,
                QuestionId = questionId,
            };
            await answerRepository.AddAsync(answer);
            question.AnswersCount++;
            await notificationServices.AddNotificationAsync(question.UserId, userId, questionId, NotificationType.AnsweredQuestion);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task DeleteAnswerAsync(int questionId, int answerId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (answer.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            if (answer.AnswerStatus == AnswerStatus.Approved)
            {
                throw new BadRequestException(AnswerExceptionMessages.CanNotDeleteOrEditApprovedAnswer);
            }
            answerRepository.Delete(answer);
            question.AnswersCount--;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<AnswerResponseDto> UpdateAnswerAsync(int questionId, int answerId, AnswerRequestDto answerRequestDto)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (answer.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            if (answer.AnswerStatus == AnswerStatus.Approved)
            {
                throw new BadRequestException(AnswerExceptionMessages.CanNotDeleteOrEditApprovedAnswer);
            }
            answer.Body = answerRequestDto.Body;
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<AnswerResponseDto>(answer);
        }

        public async Task ApproveAnswerAsync(int questionId, int answerId)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != question.Id)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }

            answer.AnswerStatus = AnswerStatus.Approved;
            question.Status = QuestionStatus.Closed;
            await notificationServices.AddNotificationAsync(answer.UserId, userId, questionId, NotificationType.ApprovedAnswer);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
