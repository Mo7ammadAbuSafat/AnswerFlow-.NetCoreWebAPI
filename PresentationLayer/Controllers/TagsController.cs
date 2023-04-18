using BusinessLayer.DTOs.TagDtos;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagServices tagServices;
        public TagsController(ITagServices tagServices)
        {
            this.tagServices = tagServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagResponseDto>>> GetAllTags()
        {
            var tags = await tagServices.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpPost]
        public async Task<ActionResult<TagResponseDto>> AddNewTag(TagRequestDto tagRequestDto)
        {
            var tag = await tagServices.AddNewTagAsync(tagRequestDto);
            return Ok(tag);
        }

        [HttpPut("{tagId}")]
        public async Task<ActionResult<TagResponseDto>> UpdateTag(int tagId, TagRequestDto tagRequestDto)
        {
            var tag = await tagServices.UpdateTagAsync(tagId, tagRequestDto);
            return Ok(tag);
        }

        [HttpDelete("{tagId}")]
        public async Task<IActionResult> DeleteTag(int tagId)
        {
            await tagServices.DeleteTagAsync(tagId);
            return Ok();
        }
    }
}
