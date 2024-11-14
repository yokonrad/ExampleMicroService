using PostMicroService.Dto;
using PostMicroService.Repositories;

namespace PostMicroService.Services.PostCommands
{
    internal class GetAllCommand(IPostRepository postRepository)
    {
        internal async Task<IEnumerable<PostDto>> Execute()
        {
            return await postRepository.GetAll();
        }
    }
}