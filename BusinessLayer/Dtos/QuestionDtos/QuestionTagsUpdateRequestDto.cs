namespace BusinessLayer.DTOs.QuestionDtos
{
    public class QuestionTagsUpdateRequestDto
    {
        public ICollection<string> TagsNames { get; set; } = new HashSet<string>();
    }
}
