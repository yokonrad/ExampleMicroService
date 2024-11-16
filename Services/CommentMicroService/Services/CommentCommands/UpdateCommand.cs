using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.CommentCommands
{
    internal class UpdateCommand(ICommentRepository commentRepository)
    {
        internal async Task<CommentDto> Execute(int id, UpdateCommentDto updateCommentDto)
        {
            return await commentRepository.Update(id, updateCommentDto);
        }
    }
}