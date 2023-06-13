using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.NotificationServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserPermissionsServices : IUserPermissionsServices
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;
        private readonly INotificationServices notificationServices;

        public UserPermissionsServices(
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices,
            INotificationServices notificationServices)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
            this.notificationServices = notificationServices;
        }

        public async Task UpdatePostingPermisstionAsync(int userId, bool newValue)
        {
            var adminId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.IsBlockedFromPosting == true && newValue == true)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyBlocked);
            }
            else if (user.IsBlockedFromPosting == false && newValue == false)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyUnblocked);
            }
            if (user.Type == UserType.Expert || user.Type == UserType.Admin)
            {
                throw new BadRequestException(UserExceptionMessages.CanNotBlock);
            }
            user.IsBlockedFromPosting = newValue;
            if (user.IsBlockedFromPosting == true)
            {
                await notificationServices.AddNotificationAsync(userId, adminId, null, NotificationType.Block);
            }
            await unitOfWork.SaveChangesAsync();
        }

    }
}
