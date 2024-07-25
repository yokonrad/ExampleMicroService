using Microsoft.AspNetCore.Mvc;
using PostMicroService.Controllers.Commands;
using PostMicroService.Dto;
using PostMicroService.Services;

namespace PostMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    public class PostController : Controller
    {
        private readonly GetAllCommand _getAllCommand;
        private readonly GetByIdCommand _getByIdCommand;
        private readonly CreateCommand _createCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly DeleteCommand _deleteCommand;
        private readonly GetByIdCommentCommand _getByIdCommentCommand;

        public PostController(PostService postService, CommentService commentService)
        {
            _getAllCommand = new(this, postService);
            _getByIdCommand = new(this, postService);
            _createCommand = new(this, postService);
            _updateCommand = new(this, postService);
            _deleteCommand = new(this, postService);
            _getByIdCommentCommand = new(this, commentService);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll() => await _getAllCommand.Execute();

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id) => await _getByIdCommand.Execute(Id);

        [HttpPost]
        public async Task<ActionResult> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, UpdatePostDto updatePostDto) => await _updateCommand.Execute(Id, updatePostDto);

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id) => await _deleteCommand.Execute(Id);

        [HttpGet("{Id}/comment")]
        public async Task<ActionResult> GetByIdComment(int Id) => await _getByIdCommentCommand.Execute(Id);
    }
}
