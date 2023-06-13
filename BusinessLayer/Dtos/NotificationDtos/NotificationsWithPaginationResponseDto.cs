namespace BusinessLayer.DTOs.NotificationDtos
{
    public class NotificationsWithPaginationResponseDto
    {
        public IEnumerable<NotificationResponseDto> notifications = new List<NotificationResponseDto>();
        public int numOfPages;
        public int currentPage;
        public int numOfNewNotification;
    }
}
