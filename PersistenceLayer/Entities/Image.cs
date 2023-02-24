namespace PersistenceLayer.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}