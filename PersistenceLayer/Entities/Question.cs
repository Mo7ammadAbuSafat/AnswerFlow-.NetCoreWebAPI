using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public int AnswersCount { get; set; } = 0;
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<QuestionVote> Votes { get; set; } = new List<QuestionVote>();
        public QuestionStatus Status { get; set; }
        public ICollection<User> QuestionSavers { get; set; } = new List<User>();
        public ICollection<QuestionHistory> EditHistory { get; set; } = new List<QuestionHistory>();
        public DateTime? LastEditDate { get; set; }
        public ICollection<QuestionReport> Reports { get; set; } = new List<QuestionReport>();
        public ICollection<AnswerReport> AnswerReports { get; set; } = new List<AnswerReport>();
        public ICollection<Keyword> Keywords { get; set; }
    }
}
