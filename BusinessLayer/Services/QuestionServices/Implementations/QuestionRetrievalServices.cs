using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.QuestionServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class QuestionRetrievalServices : IQuestionRetrievalServices
    {
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public QuestionRetrievalServices(IQuestionRepository questionRepository, IMapper mapper, IBasedRepositoryServices basedRepositoryServices)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task<QuestionsWithPaginationResponseDto> GetFilteredQuestionsWithPaginationAsync
        (
        int pageNumber,
        int pageSize,
        int? userId = null,
           string? sortBy = null,
           DateTime? dateTime = null,
           QuestionStatus? questionStatus = null,
           ICollection<string>? tagNames = null
           )
        {
            IQueryable<Question> questions = await questionRepository.GetIQueryableQuestions();

            if (userId != null)
            {
                questions = questions.Where(q => q.User != null && q.User.Id == userId);
            }
            if (tagNames != null && tagNames.Count != 0)
            {
                questions = questions.Where(q => q.Tags.Any(t => tagNames.Contains(t.Name)));
            }
            if (dateTime != null)
            {
                questions = questions.Where(q => q.CreationDate >= dateTime);
            }
            if (questionStatus != null)
            {
                questions = questions.Where(q => q.Status == questionStatus);
            }
            if (sortBy != null)
            {
                switch (sortBy)
                {
                    case "date":
                        questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                    case "topVoted":
                        questions = questions.OrderByDescending(q => q.Votes.Count);
                        break;
                    case "topAnswered":
                        questions = questions.OrderByDescending(q => q.Answers.Count);
                        break;
                    default:
                        questions = questions = questions.OrderByDescending(q => q.CreationDate);
                        break;
                }
            }
            else questions = questions.OrderByDescending(q => q.CreationDate);

            var numOfPages = Math.Ceiling(questions.Count() / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                throw new BadRequestException(PaginationExceptionMessages.EnteredPageNumberExceedPagesCount);
            }
            var finalQuestions = await questions
            .Skip((pageNumber - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToListAsync();
            var result = new QuestionsWithPaginationResponseDto()
            {
                questions = mapper.Map<IEnumerable<QuestionResponseDto>>(finalQuestions),
                currentPage = pageNumber,
                numOfPages = (int)numOfPages
            };
            return result;
        }

        public async Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            return mapper.Map<QuestionResponseDto>(question);
        }
    }
}
