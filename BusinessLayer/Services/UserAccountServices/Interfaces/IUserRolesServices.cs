using PersistenceLayer.Enums;

namespace BusinessLayer.Services.UserAccountServices.Interfaces
{
    public interface IUserRolesServices
    {
        Task UpdateRoleForUser(int userId, UserType newType);
    }
}