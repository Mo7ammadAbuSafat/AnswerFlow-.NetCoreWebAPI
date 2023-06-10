using BusinessLayer.ExceptionMessages;
using BusinessLayer.Exceptions;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.BasedRepositoryServices.Interfaces;
using BusinessLayer.Services.GeneralServices;
using BusinessLayer.Services.ImageServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using Microsoft.AspNetCore.Http;
using PersistenceLayer.Entities;
using PersistenceLayer.Repositories.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserProfilePictureServices : IUserProfilePictureServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasedRepositoryServices basedRepositoryServices;
        private readonly IFileServices fileServices;
        private readonly ICloudinaryServices cloudinaryServices;
        private readonly IAuthenticatedUserServices authenticatedUserServices;


        public UserProfilePictureServices(
            IUnitOfWork unitOfWork,
            IBasedRepositoryServices basedRepositoryServices,
            IFileServices fileServices,
            ICloudinaryServices cloudinaryServices,
            IAuthenticatedUserServices authenticatedUserServices)
        {
            this.unitOfWork = unitOfWork;
            this.basedRepositoryServices = basedRepositoryServices;
            this.cloudinaryServices = cloudinaryServices;
            this.fileServices = fileServices;
            this.authenticatedUserServices = authenticatedUserServices;
        }

        public async Task ChangeProfilePictureAsync(int userId, IFormFile image)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            var imageLocalPath = await fileServices.StoreImageToLocalFolder(image);
            if (user.Image == null)
            {
                throw new BadRequestException(UserExceptionMessages.DoNotHaveProfilePicture);
            }
            await cloudinaryServices.DeleteImageFromCloudinary(user.Image.CloudinaryIdentifier);
            var upludeResults = await cloudinaryServices.UploadImageToCloudinary(imageLocalPath);

            user.Image = new Image()
            {
                ImagePath = upludeResults.Item1,
                CloudinaryIdentifier = upludeResults.Item2,
            };
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddProfilePictureAsync(int userId, IFormFile image)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.Image != null)
            {
                throw new BadRequestException(UserExceptionMessages.AlreadyHaveProfilePicture);
            }
            var imageLocalPath = await fileServices.StoreImageToLocalFolder(image);
            var upludeResults = await cloudinaryServices.UploadImageToCloudinary(imageLocalPath);
            user.Image = new Image()
            {
                ImagePath = upludeResults.Item1,
                CloudinaryIdentifier = upludeResults.Item2,
            };
            await unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteProfilePictureAsync(int userId)
        {
            var authenticatedUserId = authenticatedUserServices.GetAuthenticatedUserIdAsync();
            if (authenticatedUserId != userId)
            {
                throw new UnauthorizedException();
            }
            var user = await basedRepositoryServices.GetNonNullUserByIdAsync(userId);
            if (user.Image == null)
            {
                throw new BadRequestException(UserExceptionMessages.DoNotHaveProfilePicture);
            }
            await cloudinaryServices.DeleteImageFromCloudinary(user.Image.CloudinaryIdentifier);
            user.Image = null;
            await unitOfWork.SaveChangesAsync();
        }

    }
}
