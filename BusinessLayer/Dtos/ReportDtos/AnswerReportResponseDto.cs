using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.UserDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.ReportDtos
{
    public class AnswerReportResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public AnswerResponseDto Answer { get; set; }
        public UserOverviewResponseDto User { get; set; }
        public ReportStatus Status { get; set; }
    }
}
