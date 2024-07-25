using CommentMicroService.Controllers.Commands;
using CommentMicroService.Dto;
using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommentMicroService.Controllers
{
    [Route("api/v1/[controller]")]
    public class CommentController : Controller
    {
        private readonly GetByIdCommand _getByIdCommand;
        private readonly GetByPostIdCommand _getByPostIdCommand;
        private readonly CreateCommand _createCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly DeleteCommand _deleteCommand;

        public CommentController(CommentService commentService)
        {
            _getByIdCommand = new(this, commentService);
            _getByPostIdCommand = new(this, commentService);
            _createCommand = new(this, commentService);
            _updateCommand = new(this, commentService);
            _deleteCommand = new(this, commentService);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById(int Id) => await _getByIdCommand.Execute(Id);

        [HttpGet("{PostId}/post")]
        public async Task<ActionResult> GetByPostId(int PostId) => await _getByPostIdCommand.Execute(PostId);

        [HttpPost]
        public async Task<ActionResult> Create(CreateCommentDto createCommentDto) => await _createCommand.Execute(createCommentDto);

        [HttpPut("{Id}")]
        public async Task<ActionResult> Update(int Id, UpdateCommentDto updateCommentDto) => await _updateCommand.Execute(Id, updateCommentDto);

        [HttpDelete("{Id}")]
        public async Task<ActionResult> Delete(int Id) => await _deleteCommand.Execute(Id);
    }
}
