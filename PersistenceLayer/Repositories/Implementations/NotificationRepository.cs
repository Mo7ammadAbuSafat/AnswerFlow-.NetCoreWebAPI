using Microsoft.EntityFrameworkCore;
using PersistenceLayer.DbContexts;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace PersistenceLayer.Repositories.Implementations
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AnswerFlowContext context;
        public NotificationRepository(AnswerFlowContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await context.Notifications.AddAsync(notification);
        }

        public async Task<IQueryable<Notification>> GetIQueryableNotificationsForUserAsync(int userId)
        {
            return await Task.FromResult(context.Notifications
                .Where(n => n.UserId == userId)
                .Include(c => c.CreatedByUser)
                    .ThenInclude(u => u.Image));
        }

        public async Task<IEnumerable<Notification>> GetNewNotificationsForUserAsync(int userId)
        {
            return await context.Notifications
                .Where(n => n.UserId == userId)
                .Where(x => x.Status == NotificationStatus.New)
                .ToArrayAsync();
        }
    }
}
