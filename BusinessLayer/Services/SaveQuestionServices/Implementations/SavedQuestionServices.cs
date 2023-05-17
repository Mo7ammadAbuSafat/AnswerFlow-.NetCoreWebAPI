using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.SaveQuestionServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.SaveQuestionServices.Implementations
{
    public class SavedQuestionServices : ISavedQuestionServices
    {

        private readonly IQuestionRepository questionRepository;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;
        private readonly IAuthenticatedUserServices authenticatedUserServices;


        public SavedQuestionServices(IQuestionRepository questionRepository,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IMapper mapper,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.questionRepository = questionRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.mapper = mapper;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task<QuestionsWithPaginationResponseDto> GetSavedQuestionsForUserByIdAsync(
            int pageNumber,
            int pageSize,
            int userId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();
            questions = questions.Where(c => c.User != null &&
                c.QuestionSavers.Any(u => u.Id == userId));

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

        public async Task SaveQuestionAsync(int userId, int questionId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);

            if (!user.SavedQuestions.Any(c => c.Id == questionId))
            {
                user.SavedQuestions.Add(question);
                await unitOfWork.SaveChangesAsync();
            }
        }

        public async Task DeleteSavedQuestionAsync(int userId, int questionId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserId();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);

            if (user.SavedQuestions.Any(c => c.Id == questionId))
            {
                user.SavedQuestions.Remove(question);
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
