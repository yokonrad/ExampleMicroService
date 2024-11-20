using CommentMicroService.Dto;
using CommentMicroService.Repositories;
using CommentMicroService.Services.PostCommands;

namespace CommentMicroService.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        private readonly CreateCommand _createCommand = new(postRepository);
        private readonly DeleteCommand _deleteCommand = new(postRepository);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}