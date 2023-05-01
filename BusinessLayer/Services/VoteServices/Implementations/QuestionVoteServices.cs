using AutoMapper;
using BusinessLayer.DTOs.VoteDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
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

        public QuestionVoteServices(IUnitOfWork unitOfWork, IMapper mapper, IBasedRepositoryServices basedRepositoryServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<IEnumerable<VoteResponseDto>> GetVotesForQuestionAsync(int questionId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var votes = question.Votes;
            return mapper.Map<IEnumerable<VoteResponseDto>>(votes);
        }

        public async Task VoteForQuestionAsync(int questionId, VoteRequestDto voteRequestDto)
        {

            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(voteRequestDto.UserId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.UserId == voteRequestDto.UserId).FirstOrDefault();
            if (vote != null)
            {
                throw new BadRequestException(VoteExceptionMessages.AlreadyVoted);
            }
            vote = new QuestionVote()
            {
                UserId = voteRequestDto.UserId,
                Type = voteRequestDto.Type,
                CreationDate = DateTime.Now
            };
            question.Votes.Add(vote);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task EditVoteForQuestionAsync(int questionId, int voteId, VoteRequestDto voteRequestDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(voteRequestDto.UserId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.Id == voteId).FirstOrDefault();
            if (vote == null || vote.UserId != voteRequestDto.UserId)
            {
                throw new NotFoundException(VoteExceptionMessages.NotFoundVote);
            }
            else if (vote.Type != voteRequestDto.Type)
            {
                vote.Type = voteRequestDto.Type;
                vote.CreationDate = DateTime.Now;
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteVoteFromQuestionAsync(int questionId, int voteId, int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var vote = question.Votes.Where(x => x.Id == voteId).FirstOrDefault();
            if (vote == null || vote.UserId != userId)
            {
                throw new NotFoundException(VoteExceptionMessages.NotFoundVote);
            }
            question.Votes.Remove(vote);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
