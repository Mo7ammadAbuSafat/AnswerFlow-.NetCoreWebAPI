using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.DTOs.VoteDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.AnswerDtos
{
    public class AnswerResponseDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public AnswerStatus AnswerStatus { get; set; }
        public int QuestionId { get; set; }
        public UserOverviewResponseDto User { get; set; }
        public ICollection<AnswerVoteResponseDto> Votes { get; set; }
        public int FinalVotesValue
        {
            get
            {
                var upVotes = Votes.Where(v => v.Type == VoteType.Up).Count();
                var downVotes = Votes.Where(v => v.Type == VoteType.Down).Count();
                return upVotes - downVotes;
            }
        }
    }
}
