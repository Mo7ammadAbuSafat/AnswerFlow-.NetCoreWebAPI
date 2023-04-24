using BusinessLayer.DTOs.ImageDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.UserDtos
{
    public class UserOverviewResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public ImageResponseDto Image { get; set; }
        public bool IsBlockedFromPosting { get; set; }
    }
}
