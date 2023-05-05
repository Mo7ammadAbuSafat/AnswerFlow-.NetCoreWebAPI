using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationServicesFacade authenticationServicesFacade;

        public AuthenticationController(IAuthenticationServicesFacade authenticationServicesFacade)
        {
            this.authenticationServicesFacade = authenticationServicesFacade;
        }

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDto userRegistration)
        {
            var user = await authenticationServicesFacade.RegisterUserAsync(userRegistration);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDto userLogin)
        {
            var token = await authenticationServicesFacade.LoginUserAsync(userLogin);
            return Ok(token);
        }

        [HttpPost("verifying-email")]
        public async Task<IActionResult> VerifyUserEmail([FromRoute] int userId, [FromQuery] string code)

        {
            var user = await authenticationServicesFacade.VerifyEmailAsync(userId, code);
            return Ok(user);
        }

        [HttpPost("verification-email-code")]
        public async Task<IActionResult> ResendEmailVerificationCode([FromRoute] int userId)

        {
            await authenticationServicesFacade.ResendVerificationCodeAsync(userId);
            return Ok("success");
        }

        [HttpPost("reset-password-code")]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery] string email)

        {
            var user = await authenticationServicesFacade.SendResetPasswordCodeAsync(email);
            return Ok(user);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordByCodeSendedToEmail([FromRoute] int userId, [FromBody] ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            await authenticationServicesFacade.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordDto);
            return Ok("success");
        }
    }
}
