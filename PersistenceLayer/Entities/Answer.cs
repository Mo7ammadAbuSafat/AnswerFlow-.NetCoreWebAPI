using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Body { get; set; }
        public ICollection<AnswerVote> Votes { get; set; } = new List<AnswerVote>();
        public DateTime CreationDate { get; set; }
        public AnswerStatus AnswerStatus { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public ICollection<AnswerReport> Reports { get; set; } = new List<AnswerReport>();
    }
}
