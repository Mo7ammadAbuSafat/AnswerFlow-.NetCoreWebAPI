using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.UserDtos
{
    public class UserRegistrationRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password length must be greater or equal 8 character")]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
