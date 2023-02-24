using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public ICollection<AnswerVote> Votes { get; set; }
        public ICollection<Replay> Replays { get; set; }
        public DateTime CreationDate { get; set; }
        public AnswerStatus AnswerStatus { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }

    }
}
