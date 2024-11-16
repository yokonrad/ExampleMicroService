using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.CommentCommands
{
    internal class GetByPostIdCommand(ICommentRepository commentRepository)
    {
        internal async Task<IEnumerable<CommentDto>> Execute(int postId)
        {
            return await commentRepository.GetByPostId(postId);
        }
    }
}