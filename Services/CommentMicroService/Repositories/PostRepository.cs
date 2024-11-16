using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Repositories.PostCommands;

namespace CommentMicroService.Repositories
{
    public class PostRepository(AppDbContext appDbContext, IMapper mapper) : IPostRepository
    {
        private readonly CreateCommand _createCommand = new(appDbContext, mapper);
        private readonly DeleteCommand _deleteCommand = new(appDbContext, mapper);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<PostDto> Delete(int id) => await _deleteCommand.Execute(id);
    }
}