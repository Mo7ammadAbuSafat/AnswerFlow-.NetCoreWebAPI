using Microsoft.AspNetCore.Http;

namespace BusinessLayer.DTOs.QuestionDtos
{
    public class QuestionRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public ICollection<string> TagsNames { get; set; } = new HashSet<string>();
    }
}
