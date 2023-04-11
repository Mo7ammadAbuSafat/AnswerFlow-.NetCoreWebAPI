namespace BusinessLayer.DTOs.SavedQuestionDtos
{
    public class SavedQuestionResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
