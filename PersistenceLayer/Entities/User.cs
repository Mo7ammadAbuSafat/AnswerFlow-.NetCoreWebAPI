using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public ICollection<User> FollowingUsers { get; set; }
        public ICollection<User> FollowerUsers { get; set; }
        public ICollection<QuestionVote> QuestionVotes { get; set; }
        public ICollection<AnswerVote> AnswerVotes { get; set; }
        public ICollection<Replay> Replays { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<QuestionReport> QuestionReports { get; set; }
        public ICollection<AnswerReport> AnswerReports { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
