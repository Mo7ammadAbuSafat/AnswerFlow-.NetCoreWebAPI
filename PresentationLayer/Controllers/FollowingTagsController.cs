using BusinessLayer.Services.FollowingServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users/{userId}/following-tags")]
    [ApiController]
    public class FollowingTagsController : ControllerBase
    {
        private readonly IFollowingTagsServices followingTagsServices;

        public FollowingTagsController(IFollowingTagsServices followingTagsServices)
        {
            this.followingTagsServices = followingTagsServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetFollowingTagsForUserById(int userId)
        {
            var tags = await followingTagsServices.GetFollowingTagsForUserByIdAsync(userId);
            return Ok(tags);
        }

        [Authorize]
        [HttpPost("{tagId}")]
        public async Task<IActionResult> FollowTag(int userId, int tagId)
        {
            await followingTagsServices.FollowTagAsync(userId, tagId);
            return Ok("successful");
        }

        [Authorize]
        [HttpDelete("{tagId}")]
        public async Task<IActionResult> unfollowTag(int userId, int tagId)
        {
            await followingTagsServices.UnfollowTagAsync(userId, tagId);
            return Ok("successful");
        }

    }
}
