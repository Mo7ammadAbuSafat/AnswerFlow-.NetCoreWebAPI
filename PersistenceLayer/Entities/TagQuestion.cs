namespace PersistenceLayer.Entities
{
    public class TagQuestion
    {
        public int Id { get; set; }
        public User UserWhoAdd { get; set; }
        public int UserWhoAddId { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
