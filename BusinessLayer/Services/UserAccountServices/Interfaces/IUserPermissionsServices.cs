namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserPermissionsServices
    {
        Task UpdatePostingPermisstionAsync(int userId, bool newValue);
    }
}