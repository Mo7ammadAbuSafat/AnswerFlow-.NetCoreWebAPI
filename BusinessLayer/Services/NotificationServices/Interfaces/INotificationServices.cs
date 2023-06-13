using BusinessLayer.DTOs.NotificationDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.NotificationServices.Interfaces
{
    public interface INotificationServices
    {
        Task AddNotificationAsync(int userId, int createdByUserId, int? questionId, NotificationType type);
        Task UpdateNotificationsStatusToOldAsync(int userId);
        Task<NotificationsWithPaginationResponseDto> GetNotificationForUserAsync(int pageNumber, int pageSize, int userId);
    }
}