using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.PostCommands
{
    internal class CreateCommand(IPostRepository postRepository)
    {
        internal async Task<bool> Execute(CreatePostDto createPostDto)
        {
            await postRepository.Create(createPostDto);

            return true;
        }
    }
}