using BusinessLayer.DTOs.UserDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.NotificationDtos
{
    public class NotificationResponseDto
    {
        public DateTime CreationDate { get; set; }
        public NotificationStatus Status { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public UserOverviewResponseDto? CreatedByUser { get; set; }
        public int? QuestionId { get; set; }
    }
}
