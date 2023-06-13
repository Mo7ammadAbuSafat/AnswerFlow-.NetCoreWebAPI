using BusinessLayer.DTOs.NotificationDtos;
using BusinessLayer.Services.NotificationServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/users/{userId}/notifications")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationServices notificationServices;

        public NotificationsController(INotificationServices notificationServices)
        {
            this.notificationServices = notificationServices;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<NotificationsWithPaginationResponseDto>> GetNotifications(
            [FromRoute] int userId,
            [FromQuery] int pageNumber,
            [FromQuery] int pageSize)
        {
            var result = await notificationServices.GetNotificationForUserAsync(pageNumber, pageSize, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdateNotificationsStatusToOld([FromRoute] int userId)
        {
            await notificationServices.UpdateNotificationsStatusToOldAsync(userId);
            return Ok();
        }
    }
}
