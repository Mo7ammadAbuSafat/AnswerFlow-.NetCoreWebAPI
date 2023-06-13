using AutoMapper;
using BusinessLayer.DTOs.NotificationDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, NotificationResponseDto>();
        }
    }
}
