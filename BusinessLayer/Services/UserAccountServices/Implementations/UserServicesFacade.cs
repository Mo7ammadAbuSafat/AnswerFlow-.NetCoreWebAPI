using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserServicesFacade : IUserServicesFacade
    {
        private readonly IUserInformationServices userInformationServices;
        private readonly IUserPasswordServices userPasswordServices;
        private readonly IUserPermissionsServices userPermissionsServices;
        private readonly IUserRolesServices userRolesServices;
        private readonly IFollowingQuestionsRetrievalServices followingQuestionsRetrievalServices;

        public UserServicesFacade(
            IUserInformationServices userInformationServices,
            IUserPasswordServices userPasswordServices,
            IUserPermissionsServices userPermissionsServices,
            IUserRolesServices userRolesServices,
            IFollowingQuestionsRetrievalServices followingQuestionsRetrievalServices
            )
        {
            this.userInformationServices = userInformationServices;
            this.userPasswordServices = userPasswordServices;
            this.userPermissionsServices = userPermissionsServices;
            this.userRolesServices = userRolesServices;
            this.followingQuestionsRetrievalServices = followingQuestionsRetrievalServices;
        }

        public async Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto)
        {
            return await userInformationServices.UpdateUserInformationAsync(userId, userInformationDto);
        }

        public async Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync()
        {
            return await userInformationServices.GetUsersAsync();
        }

        public async Task<UserOverviewResponseDto> GetUserByEmailAsync(string email)
        {
            return await userInformationServices.GetUserByEmailAsync(email);
        }

        public async Task<FullUserResponseDto> GetFullUserByIdAsync(int userId)
        {
            return await userInformationServices.GetFullUserByIdAsync(userId);
        }

        public async Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto)
        {
            await userPasswordServices.ChangePasswordAsync(userId, changePasswordDto);
        }

        public async Task BlockUserFromPostingAsync(int userId)
        {
            await userPermissionsServices.BlockUserFromPostingAsync(userId);
        }

        public async Task UnblockUserFromPostingAsync(int userId)
        {
            await userPermissionsServices.UnblockUserFromPostingAsync(userId);
        }

        public async Task UpgradeUserToExpertAsync(int userId)
        {
            await userRolesServices.UpgradeUserToExpertAsync(userId);
        }

        public async Task UpgradeUserToAdminAsync(int userId)
        {
            await userRolesServices.UpgradeUserToAdminAsync(userId);
        }

        public async Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
           int pageNumber,
           int pageSize,
           int userId)
        {
            return await followingQuestionsRetrievalServices.GetFollowingQuestionsForUserByIdAsync(pageNumber, pageSize, userId);
        }
    }
}
