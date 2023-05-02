namespace BusinessLayer.DTOs.ReportDtos
{
    public class QuestionReportRequestDto
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public string Description { get; set; }
    }
}
