using AutoMapper;
using BusinessLayer.DTOs.outgoing;
using PersistenceLayer.Entities;

namespace AnswerFlow.API.Porfiles
{
    public class QuestionVoteProfile : Profile
    {
        public QuestionVoteProfile()
        {
            CreateMap<QuestionVote, QuestionVoteDto>();
        }
    }
}
