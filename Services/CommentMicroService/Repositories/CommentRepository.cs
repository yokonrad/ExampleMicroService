using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Repositories.CommentCommands;

namespace CommentMicroService.Repositories
{
    public class CommentRepository(AppDbContext appDbContext, IMapper mapper) : ICommentRepository
    {
        private readonly GetByIdCommand _getByIdCommand = new(appDbContext, mapper);
        private readonly GetByPostIdCommand _getByPostIdCommand = new(appDbContext, mapper);
        private readonly CreateCommand _createCommand = new(appDbContext, mapper);
        private readonly UpdateCommand _updateCommand = new(appDbContext, mapper);
        private readonly DeleteCommand _deleteCommand = new(appDbContext, mapper);

        public async Task<CommentDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<IEnumerable<CommentDto>> GetByPostId(int postId) => await _getByPostIdCommand.Execute(postId);

        public async Task<CommentDto> Create(CreateCommentDto createCommentDto) => await _createCommand.Execute(createCommentDto);

        public async Task<CommentDto> Update(int id, UpdateCommentDto updateCommentDto) => await _updateCommand.Execute(id, updateCommentDto);

        public async Task<CommentDto> Delete(int id) => await _deleteCommand.Execute(id);
    }
}