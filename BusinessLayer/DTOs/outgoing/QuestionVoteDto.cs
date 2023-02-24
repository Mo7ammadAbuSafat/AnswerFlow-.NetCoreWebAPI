using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.outgoing
{
    public class QuestionVoteDto
    {
        public int Id { get; set; }
        public VoteType Type { get; set; }
    }
}
