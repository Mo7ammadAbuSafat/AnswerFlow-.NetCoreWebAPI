using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.FollowingServices.Implementations
{
    public class FollowingQuestionsRetrievalServices : IFollowingQuestionsRetrievalServices
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;

        public FollowingQuestionsRetrievalServices(IQuestionRepository questionRepository, IBasedRepositoryServices basedRepositoryServices, IMapper mapper)
        {
            this.questionRepository = questionRepository;
            this.basedRepositoryServices = basedRepositoryServices;
            this.mapper = mapper;
        }

        public async Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
            int pageNumber,
            int pageSize,
            int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);

            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();
            questions = questions.Where(c => c.User != null &&
                                       (c.User.FollowerUsers.Any(u => u.Id == userId) ||
                                        c.Tags.Any(t => t.Users.Any(u => u.Id == userId))));

            var numOfPages = Math.Ceiling(questions.Count() / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                throw new BadRequestException(PaginationExceptionMessages.EnteredPageNumberExceedPagesCount);
            }

            var finalQuestions = await questions
                                        .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .OrderByDescending(c => c.CreationDate)
                                        .ToListAsync();
            var result = new QuestionsWithPaginationResponseDto()
            {
                questions = mapper.Map<IEnumerable<QuestionResponseDto>>(finalQuestions),
                currentPage = pageNumber,
                numOfPages = (int)numOfPages
            };

            return result;
        }
    }
}
