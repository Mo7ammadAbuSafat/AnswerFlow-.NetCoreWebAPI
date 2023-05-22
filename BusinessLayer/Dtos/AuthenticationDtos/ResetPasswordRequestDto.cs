using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.AuthenticationDtos
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Password length must be greater or equal 8 character")]
        public string NewPassword { get; set; } = string.Empty;

        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = string.Empty;

    }
}
