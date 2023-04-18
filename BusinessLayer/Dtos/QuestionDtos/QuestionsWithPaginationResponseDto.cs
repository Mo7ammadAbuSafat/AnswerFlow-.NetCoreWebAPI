namespace BusinessLayer.DTOs.QuestionDtos
{
    public class QuestionsWithPaginationResponseDto
    {
        public IEnumerable<QuestionResponseDto> questions = new List<QuestionResponseDto>();
        public int numOfPages;
        public int currentPage;
    }
}
