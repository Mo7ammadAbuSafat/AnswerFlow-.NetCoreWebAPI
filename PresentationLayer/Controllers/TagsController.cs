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
    }
}
