using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Services.PostCommands;

namespace CommentMicroService.Services
{
    public class PostService(AppDbContext appDbContext, IMapper mapper)
    {
        private readonly CreateCommand _createCommand = new(appDbContext, mapper);
        private readonly DeleteCommand _deleteCommand = new(appDbContext);

        public async Task<bool> Create(PostDto postDto) => await _createCommand.Execute(postDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}