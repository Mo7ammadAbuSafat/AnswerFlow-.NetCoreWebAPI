using AutoMapper;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class AnswerVoteDto : Profile
    {
        public AnswerVoteDto()
        {
            CreateMap<AnswerVote, AnswerVoteDto>();
        }
    }
}
