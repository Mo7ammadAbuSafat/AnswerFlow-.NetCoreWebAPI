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
    public class QuestionVoteServices : IQuestionVoteServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public QuestionVoteServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<IEnumerable<VoteResponseDto>> GetVotesForQuestionAsync(int questionId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var votes = question.Votes;
            return mapper.Map<IEnumerable<VoteResponseDto>>(votes);
        }

        public async Task<VoteResponseDto> VoteForQuestionAsync(int questionId, VoteRequestDto voteRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserId();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.UserId == userId).FirstOrDefault();
            if (vote != null)
            {
                throw new BadRequestException(VoteExceptionMessages.AlreadyVoted);
            }
            vote = new QuestionVote()
            {
                UserId = userId,
                Type = voteRequestDto.Type,
                CreationDate = DateTime.Now
            };
            question.Votes.Add(vote);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<VoteResponseDto>(vote);
        }

        public async Task<VoteResponseDto> EditVoteForQuestionAsync(int questionId, int voteId, VoteRequestDto voteRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserId();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.Id == voteId).FirstOrDefault();
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

        public async Task DeleteVoteFromQuestionAsync(int questionId, int voteId)
        {

            var userId = authenticatedUserServices.GetAuthenticatedUserId();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.Id == voteId).FirstOrDefault();
            if (vote == null)
            {
                throw new NotFoundException(VoteExceptionMessages.NotFoundVote);
            }
            if (vote.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            question.Votes.Remove(vote);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
