using BusinessLayer.DTOs.NotificationDtos;
using PersistenceLayer.Enums;

namespace BusinessLayer.Services.NotificationServices.Implementations
{
    public class NotificationMessageServices
    {
        public static void GenerateNotificationMessages(ref IEnumerable<NotificationResponseDto> notifications)
        {
            foreach (var notification in notifications)
            {
                string username = string.Empty;
                if (notification.CreatedByUser != null)
                {
                    username = notification.CreatedByUser.Username;
                }

                if (notification.Type == NotificationType.VoteQuestion)
                {
                    notification.Message = $"{username} vote for your question";
                }
                else if (notification.Type == NotificationType.VoteAnswer)
                {
                    notification.Message = $"{username} vote for your answer";
                }
                else if (notification.Type == NotificationType.AnsweredQuestion)
                {
                    notification.Message = $"{username} answer your question";
                }
                else if (notification.Type == NotificationType.Follow)
                {
                    notification.Message = $"{username} followed you";
                }
                else if (notification.Type == NotificationType.ApprovedAnswer)
                {
                    notification.Message = $"{username} approve your answer";
                }
                else if (notification.Type == NotificationType.Block)
                {
                    notification.Message = $"you blocked from posting questions and answers";
                }
            }
        }
    }
}
