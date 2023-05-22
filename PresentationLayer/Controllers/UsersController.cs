using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.UserAccountServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersistenceLayer.Enums;

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

        [Authorize]
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUserInformation([FromRoute] int userId, [FromBody] UserInformationToUpdateRequestDto userInformationDto)
        {
            var user = await userServicesFacade.UpdateUserInformationAsync(userId, userInformationDto);
            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}/posting-permissions")]
        public async Task<IActionResult> UpdatePostingPermisstion(int userId, bool newValue)
        {
            await userServicesFacade.UpdatePostingPermisstionAsync(userId, newValue);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{userId}/roles")]
        public async Task<IActionResult> UpdateUserRole(int userId, UserType newType)
        {
            await userServicesFacade.UpdateRoleForUser(userId, newType);
            return Ok();
        }

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
