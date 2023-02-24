using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.outgoing
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public ImageDto Image { get; set; }
        public ICollection<AnswerVoteDto> Votes { get; set; }
        public int VoteCount
        {
            get
            {
                return Votes.Count;
            }
        }
        public int VotesFinalValue
        {
            get
            {
                int upVotes = Votes.Where(v => v.Type == VoteType.Up).ToList().Count;
                int downVote = Votes.Where(v => v.Type == VoteType.Down).ToList().Count;
                return upVotes - downVote;
            }
        }
        public ICollection<ReplayDto> Replays { get; set; }
        public DateTime CreationDate { get; set; }
        public AnswerStatus AnswerStatus { get; set; }
        public UserOverviewDto User { get; set; }
    }
}