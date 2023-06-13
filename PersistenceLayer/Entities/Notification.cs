using PersistenceLayer.Enums;

namespace PersistenceLayer.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public NotificationType Type { get; set; }
        public DateTime CreationDate { get; set; }
        public NotificationStatus Status { get; set; } = NotificationStatus.New;
        public User? CreatedByUser { get; set; }
        public int? CreatedByUserId { get; set; }
        public Question? Question { get; set; }
        public int? QuestionId { get; set; }
    }
}
