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

        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public UserPermissionsServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task BlockUserFromPostingAsync(int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.IsBlockedFromPosting == true)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyBlocked);
            }
            if (user.Type == UserType.Expert || user.Type == UserType.Admin)
            {
                throw new BadRequestException(UserExceptionMessages.CanNotBlock);
            }

            user.IsBlockedFromPosting = true;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UnblockUserFromPostingAsync(int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.IsBlockedFromPosting == false)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyUnblocked);
            }
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
