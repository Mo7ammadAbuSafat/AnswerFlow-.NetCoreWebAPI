using AutoMapper;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.QuestionServices.Interfaces;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class UpdateQuestionServices : IUpdateQuestionServices
    {

        private readonly ITagRepository tagRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IMapper mapper;

        public UpdateQuestionServices(
            ITagRepository tagRepository,
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IMapper mapper)
        {
            this.tagRepository = tagRepository;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.mapper = mapper;
        }

        public async Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var dateNow = DateTime.Now;
            var editHistory = new QuestionHistory()
            {
                Title = question.Title,
                Body = question.Body,
                TagNames = string.Join(", ", question.Tags.Select(t => t.Name).ToArray()),
                EditDate = dateNow,
            };
            var tags = await tagRepository.GetTagsByNamesAsync(questionUpdateRequestDto.TagsNames);
            question.Title = questionUpdateRequestDto.Title;
            question.Body = questionUpdateRequestDto.Body;
            question.Tags = tags;
            question.LastEditDate = dateNow;
            question.EditHistory.Add(editHistory);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<QuestionResponseDto>(question);
        }

        public async Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto)
        {
            var question = await basedRepositoryServices.GetNonNullQuestionByIdAsync(questionId);
            var dateNow = DateTime.Now;
            var editHistory = new QuestionHistory()
            {
                Title = question.Title,
                Body = question.Body,
                TagNames = string.Join(", ", question.Tags.Select(t => t.Name).ToArray()),
                EditDate = dateNow,
            };
            var tags = await tagRepository.GetTagsByNamesAsync(questionTagsUpdateRequestDto.TagsNames);
            question.Tags = tags;
            question.LastEditDate = dateNow;
            question.EditHistory.Add(editHistory);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<QuestionResponseDto>(question);
        }
    }
}
