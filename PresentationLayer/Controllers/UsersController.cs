using BusinessLayer.DTOs.QuestionDtos;
using BusinessLayer.DTOs.StatisticsDtos;
using BusinessLayer.DTOs.UserDtos;
using BusinessLayer.Services.UserAccountServices.Interfaces;
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

        [HttpPost("registration")]
        public async Task<IActionResult> RegisterUser(UserRegistrationRequestDto userRegistration)
        {
            var user = await userServicesFacade.RegisterUserAsync(userRegistration);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginRequestDto userLogin)
        {
            var user = await userServicesFacade.LoginUserAsync(userLogin);
            return Ok(user);
        }

        [HttpPost("verifying-email")]
        public async Task<IActionResult> VerifyUserEmail([FromRoute] int userId, [FromQuery] string code)

        {
            var user = await userServicesFacade.VerifyEmailAsync(userId, code);
            return Ok(user);
        }

        [HttpPost("verification-email-code")]
        public async Task<IActionResult> ResendEmailVerificationCode([FromRoute] int userId)

        {
            await userServicesFacade.ResendVerificationCodeAsync(userId);
            return Ok("success");
        }

        [HttpPost("reset-password-code")]
        public async Task<IActionResult> SendResetPasswordCode([FromQuery] string email)

        {
            var user = await userServicesFacade.SendResetPasswordCodeAsync(email);
            return Ok(user);
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPasswordByCodeSendedToEmail([FromRoute] int userId, [FromBody] ResetPasswordWithCodeRequestDto resetPasswordDto)
        {
            await userServicesFacade.ResetPasswordByCodeSendedToEmailAsync(userId, resetPasswordDto);
            return Ok("success");
        }

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

        [HttpGet("statistics")]
        public async Task<ActionResult<UsersStatisticsResponseDto>> GetUsersStatistics()
        {
            var statistics = await userServicesFacade.GetUsersStatisticsAsync();
            return Ok(statistics);
        }

        [HttpGet("{userId}/calendar-statistics")]
        public async Task<ActionResult<IEnumerable<int>>> GetUserActivityCurrentYearStatistic(int userId)
        {
            var calendar = await userServicesFacade.GetUserActivityCurrentYearStatisticAsync(userId);
            return Ok(calendar);
        }

        [HttpGet("{userId}/statistics")]
        public async Task<ActionResult<UserStatisticsResponseDto>> GetUserStatistics(int userId)
        {
            var statistics = await userServicesFacade.GetUserStatisticsAsync(userId);
            return Ok(statistics);
        }

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
