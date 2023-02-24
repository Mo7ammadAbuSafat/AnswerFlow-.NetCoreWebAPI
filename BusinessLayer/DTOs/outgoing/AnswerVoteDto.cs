using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.outgoing
{
    public class AnswerVoteDto
    {
        public int Id { get; set; }
        public VoteType Type { get; set; }
    }
}