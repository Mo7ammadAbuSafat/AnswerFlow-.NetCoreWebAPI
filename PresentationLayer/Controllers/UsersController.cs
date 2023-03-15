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
            bool success = await userServices.RegisterUserAsync(userRegistration);
            if (!success)
            {
                return BadRequest("SomeThingWrong");

            }
            return Ok("Succesful Registraion");
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDto userLogin)
        {
            bool success = await userServices.LoginUserAsync(userLogin);
            if (!success)
            {
                return BadRequest("SomeThingWrong");
            }
            return Ok("Succesful Login");
        }

        [HttpPost("verifying-email")]
        public async Task<IActionResult> VerifyUserEmail(string token)
        {
            bool success = await userServices.VerifyEmailAsync(token);
            if (!success)
            {
                return BadRequest("SomeThingWrong");
            }
            return Ok("Successful verified");
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
