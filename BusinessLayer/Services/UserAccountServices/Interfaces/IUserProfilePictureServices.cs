using Microsoft.AspNetCore.Http;
namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserProfilePictureServices
    {
        Task ChangeProfilePictureAsync(int userId, IFormFile image);
        Task DeleteProfilePictureAsync(int userId);
        Task AddProfilePictureAsync(int userId, IFormFile image);
    }
}