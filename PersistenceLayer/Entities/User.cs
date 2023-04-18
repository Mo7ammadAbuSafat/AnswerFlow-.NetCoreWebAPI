using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class User
    {
        public int Id { get; set; }
        public UserType Type { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string VerificationCode { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string ResetPasswordCode { get; set; }
        public DateTime? ResetPasswordCodeExpiresDate { get; set; }
        public string About { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public ICollection<User> FollowingUsers { get; set; } = new List<User>();
        public ICollection<User> FollowerUsers { get; set; } = new List<User>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Question> SavedQuestions { get; set; } = new List<Question>();
        public ICollection<QuestionVote> QuestionVotes { get; set; } = new List<QuestionVote>();
        public ICollection<AnswerVote> AnswerVotes { get; set; } = new List<AnswerVote>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<QuestionReport> QuestionReports { get; set; } = new List<QuestionReport>();
    }
}
