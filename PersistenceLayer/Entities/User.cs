using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationCode { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string? ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiresDate { get; set; }
        public string About { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public int? ImageId { get; set; }
        public Image? Image { get; set; }
        public ICollection<User> FollowingUsers { get; set; }
        public ICollection<User> FollowerUsers { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Question> SavedQuestions { get; set; }
        public ICollection<QuestionVote> QuestionVotes { get; set; }
        public ICollection<AnswerVote> AnswerVotes { get; set; }
        public ICollection<Replay> Replays { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<QuestionReport> QuestionReports { get; set; }
        public ICollection<AnswerReport> AnswerReports { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
