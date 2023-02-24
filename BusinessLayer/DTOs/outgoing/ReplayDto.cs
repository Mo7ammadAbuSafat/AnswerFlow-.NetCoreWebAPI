namespace BusinessLayer.DTOs.outgoing
{
    public class ReplayDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public ImageDto Image { get; set; }
        public DateTime CreationDate { get; set; }
        public UserOverviewDto User { get; set; }
    }
}