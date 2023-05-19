using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserPermissionsServices : IUserPermissionsServices
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public UserPermissionsServices(IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task UpdatePostingPermisstionAsync(int userId, bool newValue)
        {
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
            await unitOfWork.SaveChangesAsync();
        }

    }
}
