namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserPermissionsServices
    {
        Task BlockUserFromPostingAsync(int userId);
        Task UnblockUserFromPostingAsync(int userId);
    }
}