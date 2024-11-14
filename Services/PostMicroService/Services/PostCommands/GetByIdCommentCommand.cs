using PostMicroService.Dto;
using PostMicroService.Repositories;

namespace PostMicroService.Services.PostCommands
{
    internal class GetByIdCommentCommand(IPostRepository postRepository)
    {
        internal async Task<PostCommentDto> Execute(int id)
        {
            return await postRepository.GetByIdComment(id);
        }
    }
}