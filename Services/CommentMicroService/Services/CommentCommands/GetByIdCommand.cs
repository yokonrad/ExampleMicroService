using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.CommentCommands
{
    internal class GetByIdCommand(ICommentRepository commentRepository)
    {
        internal async Task<CommentDto> Execute(int id)
        {
            return await commentRepository.GetById(id);
        }
    }
}