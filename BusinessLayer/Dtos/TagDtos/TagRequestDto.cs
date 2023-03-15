using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.TagDtos
{
    public class TagRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
