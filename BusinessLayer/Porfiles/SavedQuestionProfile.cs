using AutoMapper;
using BusinessLayer.DTOs.SavedQuestionDtos;
using PersistenceLayer.Entities;

namespace BusinessLayer.Porfiles
{
    public class SavedQuestionProfile : Profile
    {
        public SavedQuestionProfile()
        {
            CreateMap<SavedQuestion, SavedQuestionResponseDto>();
        }
    }
}
