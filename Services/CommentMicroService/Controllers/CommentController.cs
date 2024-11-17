using CommentMicroService.Dto;
using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentMicroService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CommentController(ICommentService commentService) : ControllerBase
    {
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CommentDto>> GetById([FromRoute] int Id) => Ok(await commentService.GetById(Id));

        [HttpGet("{PostId:int}/post")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetByPostId([FromRoute] int PostId) => Ok(await commentService.GetByPostId(PostId));

        [HttpPost]
        public async Task<ActionResult<CommentDto>> Create([FromQuery] CreateCommentDto createCommentDto) => Ok(await commentService.Create(createCommentDto));

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<CommentDto>> Update([FromRoute] int Id, [FromQuery] UpdateCommentDto updateCommentDto) => Ok(await commentService.Update(Id, updateCommentDto));

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int Id) => Ok(await commentService.Delete(Id));
    }
}