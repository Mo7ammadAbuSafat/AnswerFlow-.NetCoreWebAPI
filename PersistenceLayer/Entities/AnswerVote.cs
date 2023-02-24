using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class AnswerVote
    {
        public int Id { get; set; }
        public VoteType Type { get; set; }
        public int? UserId { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
