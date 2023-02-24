namespace PersistenceLayer.Entities
{
    public class Replay
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public DateTime CreationDate { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
