using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.ReportDtos
{
    public class ReportResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public QuestionResponseDto Question { get; set; }
        public AnswerResponseDto Answer { get; set; }
        public string ContentType
        {
            get
            {
                return Question != null ? "question" : "answer";
            }
        }
        public UserOverviewResponseDto User { get; set; }
        public ReportStatus Status { get; set; }
    }
}
