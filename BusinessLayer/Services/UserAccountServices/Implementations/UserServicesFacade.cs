﻿using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.FollowingServices.Interfaces;
using BusinessLayer.Services.UserAccountServices.Interfaces;

namespace BusinessLayer.Services.UserAccountServices.Implementations
{
    public class UserServicesFacade : IUserServicesFacade
    {
        private readonly IUserLoginServices userLoginServices;
        private readonly IUserInformationServices userInformationServices;
        private readonly IUserPasswordServices userPasswordServices;
        private readonly IUserPermissionsServices userPermissionsServices;
        private readonly IUserRegistrationServices userRegistrationServices;
        private readonly IUserStatisticsServices userStatisticsServices;
        private readonly IUserRolesServices userRolesServices;
        private readonly IFollowingQuestionsRetrievalServices followingQuestionsRetrievalServices;

        public UserServicesFacade(
            IUserLoginServices userLoginServices,
            IUserInformationServices userInformationServices,
            IUserPasswordServices userPasswordServices,
            IUserPermissionsServices userPermissionsServices,
            IUserRegistrationServices userRegistrationServices,
            IUserStatisticsServices userStatisticsServices,
            IUserRolesServices userRolesServices,
            IFollowingQuestionsRetrievalServices followingQuestionsRetrievalServices
            )
        {
            this.userLoginServices = userLoginServices;
            this.userInformationServices = userInformationServices;
            this.userPasswordServices = userPasswordServices;
            this.userPermissionsServices = userPermissionsServices;
            this.userRegistrationServices = userRegistrationServices;
            this.userStatisticsServices = userStatisticsServices;
            this.userRolesServices = userRolesServices;
            this.followingQuestionsRetrievalServices = followingQuestionsRetrievalServices;
        }

        public async Task<UserOverviewResponseDto> RegisterUserAsync(UserRegistrationRequestDto userRegistration)
        {
            return await userRegistrationServices.RegisterUserAsync(userRegistration);
        }

        public async Task<UserOverviewResponseDto> VerifyEmailAsync(int userId, string code)
        {
            return await userRegistrationServices.VerifyEmailAsync(userId, code);
        }

        public async Task ResendVerificationCodeAsync(int userId)
        {
            await userRegistrationServices.ResendVerificationCodeAsync(userId);
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

        public async Task<string> LoginUserAsync(UserLoginRequestDto userLogin)
        {
            return await userLoginServices.LoginUserAsync(userLogin);
        }

        public async Task<UserOverviewResponseDto> SendResetPasswordCodeAsync(string email)
        {
            return await userPasswordServices.SendResetPasswordCodeAsync(email);
        }

        public async Task ResetPasswordByCodeSendedToEmailAsync(int userId, ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            await userPasswordServices.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordDto);
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

        public async Task<UsersStatisticsResponseDto> GetUsersStatisticsAsync()
        {
            return await userStatisticsServices.GetUsersStatisticsAsync();
        }

        public async Task<IEnumerable<string>> GetUserActivityCurrentYearStatisticAsync(int userId)
        {
            return await userStatisticsServices.GetUserActivityCurrentYearStatisticAsync(userId);
        }

        public async Task<UserStatisticsResponseDto> GetUserStatisticsAsync(int userId)
        {
            return await userStatisticsServices.GetUserStatisticsAsync(userId);
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
