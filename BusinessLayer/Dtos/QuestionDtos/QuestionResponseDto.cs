using BusinessLayer.DTOs.AnswerDtos;
using BusinessLayer.DTOs.SavedQuestionDtos;
using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.DTOs.VoteDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.QuestionDtos
{
    public class QuestionResponseDto
    {
        public int Id { get; set; }
        public UserOverviewResponseDto User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<TagResponseDto> Tags { get; set; }

        public ICollection<SavedQuestionResponseDto> QuestionSavers { get; set; }

        public ICollection<QuestionVoteResponseDto> Votes { get; set; }
        public int FinalVotesValue
        {
            get
            {
                var upVotes = Votes.Where(v => v.Type == VoteType.Up).Count();
                var downVotes = Votes.Where(v => v.Type == VoteType.Down).Count();
                return upVotes - downVotes;
            }
        }
        public ICollection<AnswerResponseDto> Answers { get; set; }
        public int AnswersCount { get; set; }
        public QuestionStatus Status { get; set; }
    }
}
