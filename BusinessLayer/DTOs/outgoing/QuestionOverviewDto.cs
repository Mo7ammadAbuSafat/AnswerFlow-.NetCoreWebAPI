using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.outgoing
{
    public class QuestionOverviewDto
    {
        public int Id { get; set; }
        public UserOverviewDto User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<TagDto> Tags { get; set; }
        public ICollection<QuestionVoteDto> Votes { get; set; }
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
        public QuestionStatus Status { get; set; }
        public DateTime? ClosedDate { get; set; }
    }
}
