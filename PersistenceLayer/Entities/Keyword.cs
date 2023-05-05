namespace PersistenceLayer.Entities
{
    public class Keyword
    {
        public int Id { get; set; }
        public string name { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
