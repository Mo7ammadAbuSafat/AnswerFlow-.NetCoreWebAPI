using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServicesFacade userServicesFacade;

        public UsersController(IUserServicesFacade userServicesFacade)
        {
            this.userServicesFacade = userServicesFacade;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await userServicesFacade.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetFullUserById(int userId)
        {
            var user = await userServicesFacade.GetFullUserByIdAsync(userId);
            return Ok(user);
        }



        [Authorize]
        [HttpPut("{userId}/change-password")]
        public async Task<IActionResult> ChangePassword([FromRoute] int userId, [FromBody] ChangePasswordRequestDto resetPasswordDto)
        {
            await userServicesFacade.ChangePasswordAsync(userId, resetPasswordDto);
            return Ok("success");
        }

        //[HttpPut("{userId}/user-information")]
        //public async Task<IActionResult> UpdateUserInformation([FromRoute] int userId, [FromBody] UserInformationToUpdateRequestDto userInformationDto)
        //{
        //    var user = await userServices.UpdateUserInformationAsync(userId, userInformationDto);
        //    return Ok(user);
        //}

        //[HttpPut("{userId}/block")]
        //public async Task<IActionResult> BlockUserFromPosting(int userId)
        //{
        //    await userServices.BlockUserFromPostingAsync(userId);
        //    return Ok();
        //}

        //[HttpPut("{userId}/unblock")]
        //public async Task<IActionResult> UnblockUserFromPosting(int userId)
        //{
        //    await userServices.UnblockUserFromPostingAsync(userId);
        //    return Ok();
        //}

        //[HttpPut("{userId}/upgrade-to-expert")]
        //public async Task<IActionResult> UpgradeUserToExpert(int userId)
        //{
        //    await userServices.UpgradeUserToExpertAsync(userId);
        //    return Ok();
        //}

        //[HttpPut("{userId}/upgrade-to-admin")]
        //public async Task<IActionResult> UpgradeUserToAdmin(int userId)
        //{
        //    await userServices.UpgradeUserToAdminAsync(userId);
        //    return Ok();
        //}

        [Authorize]
        [HttpGet("{userId}/feed")]
        public async Task<ActionResult<IEnumerable<QuestionsWithPaginationResponseDto>>> GetFollowingQuestionsForUserById(
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize,
            [FromRoute] int userId)
        {
            var questions = await userServicesFacade.GetFollowingQuestionsForUserByIdAsync(pageNumber, pageSize, userId);
            return Ok(questions);
        }
    }
}
