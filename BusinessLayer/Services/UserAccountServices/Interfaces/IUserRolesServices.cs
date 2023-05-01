namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserRolesServices
    {
        Task UpgradeUserToAdminAsync(int userId);
        Task UpgradeUserToExpertAsync(int userId);
    }
}