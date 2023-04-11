using AutoMapper;
using BusinessLayer.DTOs.VoteDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class QuestionVoteProfile : Profile
    {
        public QuestionVoteProfile()
        {
            CreateMap<QuestionVote, QuestionVoteResponseDto>();
        }
    }
}
