namespace PersistenceLayer.Entities
{
    public class SavedQuestion
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
