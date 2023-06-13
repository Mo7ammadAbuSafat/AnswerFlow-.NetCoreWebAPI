using PersistenceLayer.Entities;

namespace PersistenceLayer.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task AddAsync(Notification notification);
        Task<IQueryable<Notification>> GetIQueryableNotificationsForUserAsync(int userId);
        Task<IEnumerable<Notification>> GetNewNotificationsForUserAsync(int userId);
    }
}