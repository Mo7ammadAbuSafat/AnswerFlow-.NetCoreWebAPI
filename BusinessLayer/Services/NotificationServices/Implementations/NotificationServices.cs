using AutoMapper;
using BusinessLayer.DTOs.NotificationDtos;
using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.NotificationServices.Interfaces;
using Microsoft.EntityFrameworkCore;
using PersistenceLayer.Entities;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.NotificationServices.Implementations
{
    public class NotificationServices : INotificationServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly INotificationRepository notificationRepository;
        private readonly IMapper mapper;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;

        public NotificationServices(IUnitOfWork unitOfWork,
            INotificationRepository notificationRepository,
            IMapper mapper,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.unitOfWork = unitOfWork;
            this.notificationRepository = notificationRepository;
            this.mapper = mapper;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task AddNotificationAsync(int userId, int createdByUserId, int? questionId, NotificationType type)
        {
            if (createdByUserId == userId) { return; }
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != createdByUserId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var createdByUser = await basedRepositoryServices.GetNonNullUserByIdAsync(createdByUserId);
            Question question = null;
            if (questionId != null)
            {
                question = await basedRepositoryServices.GetNonNullQuestionByIdAsync((int)questionId);
            }

            var notification = new Notification()
            {
                User = user,
                CreatedByUser = createdByUser,
                CreationDate = DateTime.Now,
                Question = question,
                Type = type
            };

            await notificationRepository.AddAsync(notification);
        }

        public async Task UpdateNotificationsStatusToOldAsync(int userId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var newNotifications = await notificationRepository.GetNewNotificationsForUserAsync(userId);
            foreach (var notification in newNotifications)
            {
                notification.Status = NotificationStatus.Old;
            }
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<NotificationsWithPaginationResponseDto> GetNotificationForUserAsync
        (
            int pageNumber,
            int pageSize,
            int userId
        )
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var notifications = await notificationRepository.GetIQueryableNotificationsForUserAsync(userId);
            var newNotificationCount = await notifications.Where(x => x.Status == NotificationStatus.New).CountAsync();
            notifications = notifications.OrderByDescending(q => q.CreationDate);
            ApplyPagination(ref notifications, pageNumber, pageSize, out double numOfPages);
            var notificationsList = await notifications.ToListAsync();
            var notificationResponseList = mapper.Map<IEnumerable<NotificationResponseDto>>(notificationsList);
            NotificationMessageServices.GenerateNotificationMessages(ref notificationResponseList);
            var result = new NotificationsWithPaginationResponseDto()
            {
                currentPage = pageNumber,
                numOfNewNotification = newNotificationCount,
                numOfPages = (int)numOfPages,
                notifications = notificationResponseList
            };
            return result;
        }

        private static void ApplyPagination(
            ref IQueryable<Notification> notifications,
            int pageNumber,
            int pageSize,
            out double numOfPages
            )
        {
            numOfPages = Math.Ceiling(notifications.Count() / (pageSize * 1f));
            if (pageNumber > numOfPages && numOfPages != 0)
            {
                throw new BadRequestException(PaginationExceptionMessages.EnteredPageNumberExceedPagesCount);
            }
            notifications = notifications.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
