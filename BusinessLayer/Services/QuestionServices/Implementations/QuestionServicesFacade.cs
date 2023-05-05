using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.Services.QuestionServices.Interfaces;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.QuestionServices.Implementations
{
    public class QuestionServicesFacade : IQuestionServicesFacade
    {
        private readonly IAddAndDeleteQuestionServices addAndDeleteQuestionServices;
        private readonly IUpdateQuestionServices updateQuestionServices;
        private readonly IQuestionRetrievalServices questionRetrievalServices;


        public QuestionServicesFacade(
            IAddAndDeleteQuestionServices addAndDeleteQuestionServices,
            IUpdateQuestionServices updateQuestionServices,
            IQuestionRetrievalServices questionRetrievalServices)
        {
            this.updateQuestionServices = updateQuestionServices;
            this.addAndDeleteQuestionServices = addAndDeleteQuestionServices;
            this.questionRetrievalServices = questionRetrievalServices;
        }

        public async Task<QuestionResponseDto> AddNewQuestionAsync(QuestionToAddRequestDto questionToAddRequestDto)
        {
            return await addAndDeleteQuestionServices.AddNewQuestionAsync(questionToAddRequestDto);
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
            await addAndDeleteQuestionServices.DeleteQuestionAsync(questionId);
        }

        public async Task<QuestionsWithPaginationResponseDto> GetQuestionsWithPaginationAsync
            (int pageNumber,
            int pageSize,
            int? userId = null,
            string? sortBy = null,
            DateTime? dateTime = null,
            QuestionStatus? questionStatus = null,
            ICollection<string>? tagNames = null,
            string? searchText = null)
        {
            return await questionRetrievalServices.GetQuestionsWithPaginationAsync
                (pageNumber,
                 pageSize,
                 userId,
                 sortBy,
                 dateTime,
                 questionStatus,
                 tagNames,
                 searchText);
        }

        public async Task<QuestionResponseDto> GetQuestionByIdAsync(int questionId)
        {
            return await questionRetrievalServices.GetQuestionByIdAsync(questionId);
        }

        public async Task<QuestionResponseDto> UpdateQuestionAsync(int questionId, QuestionUpdateRequestDto questionUpdateRequestDto)
        {
            return await updateQuestionServices.UpdateQuestionAsync(questionId, questionUpdateRequestDto);
        }

        public async Task<QuestionResponseDto> UpdateQuestionTagsAsync(int questionId, QuestionTagsUpdateRequestDto questionTagsUpdateRequestDto)
        {
            return await updateQuestionServices.UpdateQuestionTagsAsync(questionId, questionTagsUpdateRequestDto);
        }

    }
}
