using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class AnswerReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ReportStatus Status { get; set; }
    }
}
