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
        private readonly IKeywordExtractorServices keywordExtractorServices;

        public QuestionRetrievalServices(
            IQuestionRepository questionRepository,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IKeywordExtractorServices keywordExtractorServices)
        {
            this.questionRepository = questionRepository;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
            this.keywordExtractorServices = keywordExtractorServices;
        }
        public async Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task<QuestionsWithPaginationResponseDto> GetQuestionsWithPaginationAsync
        (
            int pageNumber,
            int pageSize,
            int? userId = null,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null,
            string? searchText = null
        )
        {
            IQueryable<Question> questions;
            if (searchText != null)
            {
                var keywords = await keywordExtractorServices.GetKeywordsAsync(searchText);
                var keywordsNames = keywords.Select(k => k.name).ToList();
                questions = await questionRepository.GetIQueryableQuestionsByKeywordsAsync(keywordsNames);
            }
            else
            {
                questions = await questionRepository.GetIQueryableQuestions();
            }
            ApplyBasicFiltraion(ref questions, userId, dateTime, questionStatus, tagNames);
            ApplySort(ref questions, searchText != null, sortBy);
            ApplyPagination(ref questions, pageNumber, pageSize, out double numOfPages);
            var finalQuestions = await questions.ToListAsync();
            var result = new QuestionsWithPaginationResponseDto()
            {
                questions = mapper.Map<IEnumerable<QuestionResponseDto>>(finalQuestions),
                currentPage = pageNumber,
                numOfPages = (int)numOfPages
            };
            return result;
        }

        private static void ApplyPagination(
            ref IQueryable<Question> questions,
            int pageNumber,
            int pageSize,
            out double numOfPages
            )
        {
            numOfPages = Math.Ceiling(questions.Count() / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                throw new BadRequestException(PaginationExceptionMessages.EnteredPageNumberExceedPagesCount);
            }
            questions = questions.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        private static void ApplyBasicFiltraion(
            ref IQueryable<Question> questions,
            int? userId = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null
            )
        {
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
        }

        private static void ApplySort(ref IQueryable<Question> questions, bool isASearch, string? sortBy = null)
        {
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
            else if (!isASearch)
            {
                questions = questions.OrderByDescending(q => q.CreationDate);
            }
        }
    }
}
