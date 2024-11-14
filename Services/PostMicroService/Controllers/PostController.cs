using Microsoft.AspNetCore.Mvc;
using PostMicroService.Dto;
using PostMicroService.Services;

namespace PostMicroService.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostController(IPostService postService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetAll() => Ok(await postService.GetAll());

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<PostDto>> GetById([FromRoute] int Id) => Ok(await postService.GetById(Id));

        [HttpGet("{Id:int}/comment")]
        public async Task<ActionResult<PostCommentDto>> GetByIdComment([FromRoute] int Id) => Ok(await postService.GetByIdComment(Id));

        [HttpPost]
        public async Task<ActionResult<PostDto>> Create([FromQuery] CreatePostDto createPostDto) => Ok(await postService.Create(createPostDto));

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<PostDto>> Update([FromRoute] int Id, [FromQuery] UpdatePostDto updatePostDto) => Ok(await postService.Update(Id, updatePostDto));

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int Id) => Ok(await postService.Delete(Id));
    }
}