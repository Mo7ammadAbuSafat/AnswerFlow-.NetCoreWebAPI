using BusinessLayer.DTOs.StatisticsDtos;
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


        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userServices.GetUsersAsync();
            return Ok(users);
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


        [HttpPut("{userId}/reset-password-by-code")]
        public async Task<IActionResult> ResetPasswordByCodeSendedToEmail([FromRoute] int userId, [FromBody] ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            await userServices.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordDto);
            return Ok("success");
        }


        [HttpPut("{userId}/reset-password-by-old-password")]
        public async Task<IActionResult> ResetPasswordByOldPassword([FromRoute] int userId, [FromBody] ResetPasswordWithOldPasswordRequestDto resetPasswordDto)
        {
            await userServices.ResetPasswordByOldPasswordAsync(userId, resetPasswordDto);
            return Ok("success");
        }


        [HttpPut("{userId}/user-information")]
        public async Task<IActionResult> UpdateUserInformation([FromRoute] int userId, [FromBody] UserInformationToUpdateRequestDto userInformationDto)
        {
            var user = await userServices.UpdateUserInformationAsync(userId, userInformationDto);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var user = await userServices.GetUserByEmailAsync(email);
            return Ok(user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFullUserById(int userId)
        {
            var user = await userServices.GetFullUserByIdAsync(userId);
            return Ok(user);
        }

        [HttpPost("{userId}/following/{followedUserId}")]
        public async Task<IActionResult> FollowUser(int userId, int followedUserId)
        {
            await userServices.FollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }


        [HttpDelete("{userId}/following/{followedUserId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followedUserId)
        {
            await userServices.UnfollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }


        [HttpGet("{userId}/following")]
        public async Task<IActionResult> GetFollowingUsersForUserById(int userId)
        {
            var users = await userServices.GetFollowingUsersForUserByIdAsync(userId);
            return Ok(users);
        }


        [HttpPost("{userId}/tags/{tagId}/following")]
        public async Task<IActionResult> FollowTag(int userId, int tagId)
        {
            await userServices.FollowTagAsync(userId, tagId);
            return Ok("successful");
        }


        [HttpDelete("{userId}/tags/{tagId}/following")]
        public async Task<IActionResult> unfollowTag(int userId, int tagId)
        {
            await userServices.UnfollowTagAsync(userId, tagId);
            return Ok("successful");
        }


        [HttpGet("{userId}/tags/following")]
        public async Task<IActionResult> GetFollowingTagsForUserById(int userId)
        {
            var tags = await userServices.GetFollowingTagsForUserByIdAsync(userId);
            return Ok(tags);
        }

        [HttpPut("{userId}/block")]
        public async Task<IActionResult> BlockUserFromPosting(int userId)
        {
            await userServices.BlockUserFromPostingAsync(userId);
            return Ok();
        }

        [HttpPut("{userId}/unblock")]
        public async Task<IActionResult> UnblockUserFromPosting(int userId)
        {
            await userServices.UnblockUserFromPostingAsync(userId);
            return Ok();
        }

        [HttpPut("{userId}/upgrade-to-expert")]
        public async Task<IActionResult> UpgradeUserToExpert(int userId)
        {
            await userServices.UpgradeUserToExpertAsync(userId);
            return Ok();
        }

        [HttpPut("{userId}/upgrade-to-admin")]
        public async Task<IActionResult> UpgradeUserToAdmin(int userId)
        {
            await userServices.UpgradeUserToAdminAsync(userId);
            return Ok();
        }

        [HttpGet("statistics")]
        public async Task<ActionResult<UsersStatisticsResponseDto>> GetUsersStatistics()
        {
            var statistics = await userServices.GetUsersStatisticsAsync();
            return Ok(statistics);
        }
    }
}
