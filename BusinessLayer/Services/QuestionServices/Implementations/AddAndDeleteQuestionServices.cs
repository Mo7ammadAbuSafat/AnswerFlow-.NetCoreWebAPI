using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using BusinessLayer.Services.ImageServices.Interfaces;
using BusinessLayer.Services.QuestionServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;
using System.Text.RegularExpressions;

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
        private readonly IAuthenticatedUserServices authenticatedUserServices;
        private readonly IFileServices fileServices;
        private readonly ICloudinaryServices cloudinaryServices;


        public AddAndDeleteQuestionServices(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IQuestionRepository questionRepository,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IKeywordExtractorServices keywordExtractorServices,
            IAuthenticatedUserServices authenticatedUserServices,
            IFileServices fileServices,
            ICloudinaryServices cloudinaryServices)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
            this.questionRepository = questionRepository;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
            this.keywordExtractorServices = keywordExtractorServices;
            this.authenticatedUserServices = authenticatedUserServices;
            this.cloudinaryServices = cloudinaryServices;
            this.fileServices = fileServices;
        }

        public async Task<QuestionResponseDto> AddNewQuestionAsync(QuestionRequestDto questionToAddRequestDto)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException(UserExceptionMessages.BlocedUserFromPosting);
            }
            questionToAddRequestDto.TagsNames = questionToAddRequestDto.TagsNames.Select(t => t.ToLower()).ToList();
            var tags = await tagRepository.GetTagsByNamesAsync(questionToAddRequestDto.TagsNames);
            var bodyWithOutHtmlFormat = Regex.Replace(questionToAddRequestDto.Body, "<.*?>", string.Empty);
            var keywords = await keywordExtractorServices.GetKeywordsAsync(questionToAddRequestDto.Title + " " + bodyWithOutHtmlFormat);
            var question = new Question()
            {
                Title = questionToAddRequestDto.Title,
                Body = questionToAddRequestDto.Body,
                Tags = tags,
                CreationDate = DateTime.Now,
                UserId = user.Id,
                Keywords = keywords
            };
            if (questionToAddRequestDto.Image != null)
            {
                var imageLocalPath = await fileServices.StoreImageToLocalFolder(questionToAddRequestDto.Image);
                var upludeResults = await cloudinaryServices.UploadImageToCloudinary(imageLocalPath);
                question.Image = new PersistenceLayer.Entities.Image()
                {
                    ImagePath = upludeResults.Item1,
                    CloudinaryIdentifier = upludeResults.Item2,
                };
            }

            await questionRepository.AddAsync(question);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            var userId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            if (question.UserId != userId)
            {
                throw new UnauthorizedException();
            }
            if (question.Status == QuestionStatus.Closed)
            {
                throw new BadRequestException(QuestionExceptionMessages.CanNotDeleteOrEditClosedQuesiton);
            }
            if (question.Image != null)
            {
                await cloudinaryServices.DeleteImageFromCloudinary(question.Image.CloudinaryIdentifier);
            }
            questionRepository.Delete(question);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
