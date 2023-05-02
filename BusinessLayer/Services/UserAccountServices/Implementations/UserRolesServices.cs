using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Enums;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserRolesServices : IUserRolesServices
    {
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;

        public UserRolesServices(IUserRepository userRepository, IUnitOfWork unitOfWork, IBasedRepositoryServices basedRepositoryServices)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
        }

        public async Task UpgradeUserToExpertAsync(int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.Type == UserType.Expert)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyExpert);
            }
            if (user.Type == UserType.Admin)
            {
                throw new BadRequestException(UserExceptionMessages.UserIsAdmin);
            }
            user.Type = UserType.Expert;
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task UpgradeUserToAdminAsync(int userId)
        {
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.Type == UserType.Admin)
            {
                throw new BadRequestException(UserExceptionMessages.UserAlreadyAdmin);
            }
            user.Type = UserType.Admin;
            user.IsBlockedFromPosting = false;
            await unitOfWork.SaveChangesAsync();
        }

        //public async Task DegradeExpertToUserAsync(int userId)
        //{
        //    var user = await userRepository.GetUserById(userId);
        //    if (user == null)
        //    {
        //        throw new NotFoundException(ExceptionMessages.NotFoundUserById);
        //    }
        //    if (user.Type == UserType.Expert)
        //    {
        //        throw new BadRequestException(ExceptionMessages.UserAlreadyExpert);
        //    }
        //    if (user.Type == UserType.Admin)
        //    {
        //        throw new BadRequestException(ExceptionMessages.UserIsAdmin);
        //    }

        //    user.Type = UserType.Expert;
        //    user.IsBlockedFromPosting = false;
        //    await unitOfWork.SaveChangesAsync();
        //}

        //public async Task DegradeAdminToUserAsync(int userId)
        //{
        //    var user = await userRepository.GetUserById(userId);
        //    if (user == null)
        //    {
        //        throw new NotFoundException(ExceptionMessages.NotFoundUserById);
        //    }
        //    if (user.Type == UserType.Admin)
        //    {
        //        throw new BadRequestException(ExceptionMessages.UserAlreadyAdmin);
        //    }

        //    user.Type = UserType.Admin;
        //    user.IsBlockedFromPosting = false;
        //    await unitOfWork.SaveChangesAsync();
        //}
    }
}
