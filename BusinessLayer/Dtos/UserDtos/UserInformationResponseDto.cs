using BusinessLayer.DTOs.ImageDtos;

namespace BusinessLayer.DTOs.UserDtos
{
    public class UserInformationResponseDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; } = string.Empty;
        public string About { get; set; } = string.Empty;
        public ImageResponseDto Image { get; set; }
    }
}
