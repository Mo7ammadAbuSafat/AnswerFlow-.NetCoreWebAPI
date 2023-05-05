using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.QuestionServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class AddAndDeleteQuestionServices : IAddAndDeleteQuestionServices
    {
        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IQuestionRepository questionRepository;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IKeywordExtractorServices keywordExtractorServices;


        public AddAndDeleteQuestionServices(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IQuestionRepository questionRepository,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IKeywordExtractorServices keywordExtractorServices)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
            this.keywordExtractorServices = keywordExtractorServices;
        }

        public async Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(questionToAddRequestDto.UserId);
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException(UserExceptionMessages.BlocedUserFromPosting);
            }
            questionToAddRequestDto.TagsNames = questionToAddRequestDto.TagsNames.Select(t => t.ToLower()).ToList();
            var tags = await tagRepository.GetTagsByNamesAsync(questionToAddRequestDto.TagsNames);
            var keywords = await keywordExtractorServices.GetKeywordsAsync(questionToAddRequestDto.Title + " " + questionToAddRequestDto.Body);
            var question = new Question()
            {
                Title = questionToAddRequestDto.Title,
                Body = questionToAddRequestDto.Body,
                Tags = tags,
                CreationDate = DateTime.Now,
                UserId = user.Id,
                Keywords = keywords
            };

            await questionRepository.AddAsync(question);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);

            questionRepository.Delete(question);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
