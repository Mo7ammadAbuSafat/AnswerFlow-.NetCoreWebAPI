using AutoMapper;
using BusinessLayer.DTOs.VoteDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.VoteServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.VoteServices.Implementations
{
    public class AnswerVoteServices : IAnswerVoteServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public AnswerVoteServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.mapper = mapper;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<IEnumerable<VoteResponseDto>> GetVotesForAnswerAsync(int questionId, int answerId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != questionId)
            {
                throw new BadRequestException(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            var votes = answer.Votes;
            return mapper.Map<IEnumerable<VoteResponseDto>>(votes);
        }

        public async Task<VoteResponseDto> VoteForAnswerAsync(int questionId, int answerId, VoteRequestDto voteRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != questionId)
            {
                throw new Exception(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            var vote = answer.Votes.Where(x => x.UserId == userId).FirstOrDefault();
            if (vote != null)
            {
                throw new BadRequestException(VoteExceptionMessages.AlreadyVoted);
            }
            vote = new AnswerVote()
            {
                UserId = userId,
                Type = voteRequestDto.Type,
                CreationDate = DateTime.Now
            };
            answer.Votes.Add(vote);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<VoteResponseDto>(vote);
        }

        public async Task<VoteResponseDto> EditVoteForAnswerAsync(int questionId, int answerId, int voteId, VoteRequestDto voteRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != questionId)
            {
                throw new Exception(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            var vote = answer.Votes.Where(x => x.Id == voteId).FirstOrDefault();
            if (vote == null)
            {
                throw new NotFoundException(VoteExceptionMessages.NotFoundVote);
            }
            if (vote.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            if (vote.Type != voteRequestDto.Type)
            {
                vote.Type = voteRequestDto.Type;
                vote.CreationDate = DateTime.Now;
            }
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<VoteResponseDto>(vote);
        }

        public async Task DeleteVoteFromAnswerAsync(int questionId, int answerId, int voteId)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var answer = await basedRepositoryServices.GetNonNullAnswerByIdAsync(answerId);
            if (answer.QuestionId != questionId)
            {
                throw new Exception(AnswerExceptionMessages.AnswerNotForQuestion);
            }
            var vote = answer.Votes.Where(x => x.Id == voteId).FirstOrDefault();
            if (vote == null)
            {
                throw new NotFoundException(VoteExceptionMessages.NotFoundVote);
            }
            if (vote.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            answer.Votes.Remove(vote);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
