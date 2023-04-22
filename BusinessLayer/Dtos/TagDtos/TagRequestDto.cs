using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.DTOs.TagDtos
{
    public class TagRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string? SourceLink { get; set; }
    }
}
