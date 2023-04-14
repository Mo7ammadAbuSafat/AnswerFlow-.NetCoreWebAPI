using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<TagQuestion> Tags { get; set; } = new List<TagQuestion>();
        public ICollection<Image> Imeges { get; set; } = new List<Image>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
        public ICollection<QuestionVote> Votes { get; set; } = new List<QuestionVote>();
        public QuestionStatus Status { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public ICollection<QuestionHistory>? QuestionHistory { get; set; } = new List<QuestionHistory>();
        public ICollection<SavedQuestion> QuestionSavers { get; set; } = new List<SavedQuestion>();
    }
}
