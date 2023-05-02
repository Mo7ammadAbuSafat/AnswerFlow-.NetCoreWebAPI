using BusinessLayer.DTOs.UserDtos;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserLoginServices
    {
        Task<string> LoginUserAsync(UserLoginRequestDto userLogin);
    }
}