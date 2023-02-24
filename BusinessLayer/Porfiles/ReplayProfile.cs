using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class ReplayProfile : Profile
    {
        public ReplayProfile()
        {
            CreateMap<Replay, ReplayDto>();
        }
    }
}
