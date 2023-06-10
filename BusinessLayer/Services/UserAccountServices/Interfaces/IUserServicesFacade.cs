using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using Microsoft.AspNetCore.Http;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserServicesFacade
    {
        Task ChangePasswordAsync(int userId, ChangePasswordRequestDto changePasswordDto);
        Task<FullUserResponseDto> GetFullUserByIdAsync(int userId);
        Task<UserOverviewResponseDto> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserOverviewResponseDto>> GetUsersAsync();
        Task UpdatePostingPermisstionAsync(int userId, bool newValue);
        Task<UserOverviewResponseDto> UpdateUserInformationAsync(int userId, UserInformationToUpdateRequestDto userInformationDto);
        Task UpdateRoleForUser(int userId, UserType newType);
        Task<QuestionsWithPaginationResponseDto> GetFollowingQuestionsForUserByIdAsync(
           int pageNumber,
           int pageSize,
           int userId);
        Task ChangeProfilePictureAsync(int userId, IFormFile image);
        Task DeleteProfilePictureAsync(int userId);
        Task AddProfilePictureAsync(int userId, IFormFile image);
    }
}