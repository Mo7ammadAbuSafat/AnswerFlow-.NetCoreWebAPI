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
        public ICollection<Tag> Tags { get; set; }
        public ICollection<Image>? Imeges { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<QuestionVote> Votes { get; set; }
        public QuestionStatus Status { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? LastEditDate { get; set; }
        public ICollection<QuestionHistory>? QuestionHistory { get; set; }
        public ICollection<User> UsersWhoSaveThisQuestion { get; set; }
    }
}
