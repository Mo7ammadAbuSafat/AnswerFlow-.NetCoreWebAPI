using AutoMapper;
using BusinessLayer.DTOs.VoteDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class AnswerVoteProfile : Profile
    {
        public AnswerVoteProfile()
        {
            CreateMap<AnswerVote, AnswerVoteResponseDto>();
        }
    }
}
