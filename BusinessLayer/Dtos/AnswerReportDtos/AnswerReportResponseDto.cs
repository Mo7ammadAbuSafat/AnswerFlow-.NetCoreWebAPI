using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.DTOs.QuestionReportDtos
{
    public class AnswerReportResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public AnswerResponseDto Answer { get; set; }
        public UserOverviewResponseDto User { get; set; }
    }
}
