using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }


        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDto userRegistration)
        {
            var user = await userServices.RegisterUserAsync(userRegistration);
            return Ok(user);
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDto userLogin)
        {
            var user = await userServices.LoginUserAsync(userLogin);
            return Ok(user);
        }


        [HttpPost("{userId}/verifying-email")]
        public async Task<IActionResult> VerifyUserEmail([FromRoute] int userId, [FromQuery] string code)

        {
            var user = await userServices.VerifyEmailAsync(userId, code);
            return Ok(user);
        }


        [HttpPost("{userId}/verification-email-code")]
        public async Task<IActionResult> ResendEmailVerificationCode([FromRoute] int userId)

        {
            await userServices.ResendVerificationCodeAsync(userId);
            return Ok("success");
        }


        [HttpPost("reset-password-code")]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery] string email)

        {
            var user = await userServices.SendResetPasswordCodeAsync(email);
            return Ok(user);
        }


        [HttpPost("{userId}/resend-reset-password-code")]
        public async Task<IActionResult> ResendResetPasswordCode([FromRoute] int userId)
        {
            await userServices.ResendResetPasswordCodeAsync(userId);
            return Ok("success");
        }


        [HttpPut("{userId}/reset-password")]
        public async Task<IActionResult> ResetPasswordByCodeSendedToEmail(int userId, [FromBody] ResetPasswordWithCodeRequestDto resetPasswordWithCodeRequestDto)
        {
            await userServices.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordWithCodeRequestDto);
            return Ok("success");
        }


        [HttpPost("{userId}/following-users/{followedUserId}")]
        public async Task<IActionResult> FollowUser(int userId, int followedUserId)
        {
            await userServices.FollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }


        [HttpDelete("{userId}/following-users/{followedUserId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followedUserId)
        {
            await userServices.UnfollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }


        [HttpGet("{userId}/following-users")]
        public async Task<IActionResult> GetFollowingUsersForUserById(int userId)
        {
            var users = await userServices.GetFollowingUsersForUserByIdAsync(userId);
            return Ok(users);
        }


        [HttpPost("{userId}/following-tags/{tagId}")]
        public async Task<IActionResult> FollowTag(int userId, int tagId)
        {
            await userServices.FollowTagAsync(userId, tagId);
            return Ok("successful");
        }


        [HttpDelete("{userId}/following-tags/{tagId}")]
        public async Task<IActionResult> unfollowTag(int userId, int tagId)
        {
            await userServices.UnfollowTagAsync(userId, tagId);
            return Ok("successful");
        }


        [HttpGet("{userId}/following-tags")]
        public async Task<IActionResult> GetFollowingTagsForUserById(int userId)
        {
            var tags = await userServices.GetFollowingTagsForUserByIdAsync(userId);
            return Ok(tags);
        }
    }
}
