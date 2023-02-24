using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserOverviewDto>();
        }
    }
}
