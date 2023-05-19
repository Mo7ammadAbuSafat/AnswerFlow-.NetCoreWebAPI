using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserRolesServices : IUserRolesServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;
        private readonly IUserRepository userRepository;


        public UserRolesServices(
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IAuthenticatedUserServices authenticatedUserServices,
            IUserRepository userRepository)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.authenticatedUserServices = authenticatedUserServices;
            this.userRepository = userRepository;
        }

        public async Task UpdateRoleForUser(int userId, UserType newType)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.Type == newType)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyHaveRole);
            }
            else if (newType == UserType.Admin)
            {
                user.Type = UserType.Admin;
                user.RoleGivenByUserId = authenticatedUserId;
            }
            else if (user.Type == UserType.Admin)
            {
                var UsersIdsThatHavePermission = await userRepository.GetUsersIdsThatHavePermissinForUser(user.Id);
                if (UsersIdsThatHavePermission.Any(u => u == authenticatedUserId))
                {
                    user.Type = newType;
                    user.RoleGivenByUserId = null;
                }
                else
                {
                    throw new BadRequestException(UserExceptionMessages.DontHavePermission);
                }
            }
            else
            {
                user.Type = newType;
            }
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }
    }
}
