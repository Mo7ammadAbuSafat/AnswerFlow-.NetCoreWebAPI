using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.UserDtos
{
    public class ResetPasswordWithOldPasswordRequestDto
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "Password length must be greater or equal 8 character")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
