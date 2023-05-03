using BusinessLayer.Services.FollowingServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users/{userId}/following-users")]
    [ApiController]
    public class FollowingUsersController : ControllerBase
    {
        private readonly IFollowingUsersServices followingUsersServices;

        public FollowingUsersController(IFollowingUsersServices followingUsersServices)
        {
            this.followingUsersServices = followingUsersServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetFollowingUsersForUserById(int userId)
        {
            var users = await followingUsersServices.GetFollowingUsersForUserByIdAsync(userId);
            return Ok(users);
        }

        [Authorize]
        [HttpPost("{followedUserId}")]
        public async Task<IActionResult> FollowUser(int userId, int followedUserId)
        {
            await followingUsersServices.FollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }

        [Authorize]
        [HttpDelete("{followedUserId}")]
        public async Task<IActionResult> UnfollowUser(int userId, int followedUserId)
        {
            await followingUsersServices.UnfollowUserAsync(userId, followedUserId);
            return Ok("successful");
        }
    }
}
