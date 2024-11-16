using CommentMicroService.Dto;
using CommentMicroService.Repositories;
using CommentMicroService.Services.CommentCommands;

namespace CommentMicroService.Services
{
    public class CommentService(ICommentRepository commentRepository) : ICommentService
    {
        private readonly GetByIdCommand _getByIdCommand = new(commentRepository);
        private readonly GetByPostIdCommand _getByPostIdCommand = new(commentRepository);
        private readonly CreateCommand _createCommand = new(commentRepository);
        private readonly UpdateCommand _updateCommand = new(commentRepository);
        private readonly DeleteCommand _deleteCommand = new(commentRepository);

        public async Task<CommentDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<IEnumerable<CommentDto>> GetByPostId(int postId) => await _getByPostIdCommand.Execute(postId);

        public async Task<CommentDto> Create(CreateCommentDto createCommentDto) => await _createCommand.Execute(createCommentDto);

        public async Task<CommentDto> Update(int id, UpdateCommentDto updateCommentDto) => await _updateCommand.Execute(id, updateCommentDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}