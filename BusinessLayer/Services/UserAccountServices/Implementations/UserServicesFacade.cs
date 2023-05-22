using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using BusinessLayer.Services.FollowingServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using PersistenceLayer.Enums;

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

        public async Task UpdatePostingPermisstionAsync(int userId, bool newValue)
        {
            await userPermissionsServices.UpdatePostingPermisstionAsync(userId, newValue);
        }

        public async Task UpdateRoleForUser(int userId, UserType newType)
        {
            await userRolesServices.UpdateRoleForUser(userId, newType);
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
