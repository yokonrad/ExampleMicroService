using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.PostCommands
{
    internal class CreateCommand(IPostRepository postRepository)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            return await postRepository.Create(createPostDto);
        }
    }
}