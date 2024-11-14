using PostMicroService.Dto;
using PostMicroService.Repositories;

namespace PostMicroService.Services.PostCommands
{
    internal class UpdateCommand(IPostRepository postRepository)
    {
        internal async Task<PostDto> Execute(int id, UpdatePostDto updatePostDto)
        {
            return await postRepository.Update(id, updatePostDto);
        }
    }
}