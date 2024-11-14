using PostMicroService.Dto;
using PostMicroService.Repositories;

namespace PostMicroService.Services.PostCommands
{
    internal class GetByIdCommand(IPostRepository postRepository)
    {
        internal async Task<PostDto> Execute(int id)
        {
            return await postRepository.GetById(id);
        }
    }
}