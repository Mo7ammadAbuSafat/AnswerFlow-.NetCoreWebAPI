namespace PersistenceLayer.Entities
{
    public class QuestionHistory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? EditDate { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}