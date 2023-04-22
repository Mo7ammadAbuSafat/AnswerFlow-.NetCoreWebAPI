using BusinessLayer.DTOs.ImageDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.DTOs.UserDtos
{
    public class FullUserResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public ImageResponseDto Image { get; set; }
        public ICollection<UserOverviewResponseDto> FollowingUsers { get; set; } = new List<UserOverviewResponseDto>();
        public ICollection<UserOverviewResponseDto> FollowerUsers { get; set; } = new List<UserOverviewResponseDto>();
    }
}
