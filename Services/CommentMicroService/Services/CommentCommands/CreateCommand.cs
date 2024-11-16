using CommentMicroService.Dto;
using CommentMicroService.Repositories;

namespace CommentMicroService.Services.CommentCommands
{
    internal class CreateCommand(ICommentRepository commentRepository)
    {
        internal async Task<CommentDto> Execute(CreateCommentDto createCommentDto)
        {
            return await commentRepository.Create(createCommentDto);
        }
    }
}