using BusinessLayer.DTOs.UserDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.VoteDtos
{
    public class QuestionVoteResponseDto
    {
        public int Id { get; set; }
        public VoteType Type { get; set; }
        public UserOverviewResponseDto User { get; set; }
    }
}
