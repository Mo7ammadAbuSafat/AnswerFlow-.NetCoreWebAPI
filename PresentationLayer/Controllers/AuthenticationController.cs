using BusinessLayer.DTOs.AuthenticationDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.AuthenticationServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Register(UserRegistrationRequestDto userRegistration)
        {
            await authenticationServicesFacade.RegisterUserAsync(userRegistration);
            return Ok("success");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto userLogin)
        {
            var token = await authenticationServicesFacade.LoginUserAsync(userLogin);
            return Ok(token);
        }

        [Authorize]
        [HttpGet("identification")]
        public async Task<IActionResult> Identification()
        {
            var user = await authenticationServicesFacade.GetUserByJwtTokenAsync();
            return Ok(user);
        }

        [HttpPost("verifying-email")]
        public async Task<IActionResult> VerifyUserEmail(VerifyEmailRequestDto verifyEmailRequestDto)

        {
            var token = await authenticationServicesFacade.VerifyEmailAsync(verifyEmailRequestDto.Email, verifyEmailRequestDto.Code);
            return Ok(token);
        }

        [HttpPost("verification-email-code")]
        public async Task<IActionResult> ResendEmailVerificationCode(AuthenticationCodeRequestDto authenticationCodeRequestDto)

        {
            await authenticationServicesFacade.ResendVerificationCodeAsync(authenticationCodeRequestDto.Email);
            return Ok("success");
        }

        [HttpPost("reset-password-code")]
        public async Task<IActionResult> SendResetPasswordCode(AuthenticationCodeRequestDto resetPasswordCodeRequestDto)

        {
            await authenticationServicesFacade.SendResetPasswordCodeAsync(resetPasswordCodeRequestDto.Email);
            return Ok("success");
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDto resetPasswordDto)
        {
            await authenticationServicesFacade.ResetPasswordAsync(resetPasswordDto);
            return Ok("success");
        }
    }
}
