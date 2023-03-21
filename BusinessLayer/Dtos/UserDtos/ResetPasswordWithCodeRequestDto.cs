using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.UserDtos
{
    public class ResetPasswordWithCodeRequestDto
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(8, ErrorMessage = "Password length must be greater or equal 8 character")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword")]
        public string CondirmNewPassword { get; set; } = string.Empty;

    }
}
