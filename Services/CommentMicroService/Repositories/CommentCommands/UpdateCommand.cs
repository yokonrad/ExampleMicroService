using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.CommentCommands
{
    internal class UpdateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundException();

            comment.Text = updateCommentDto.Text ?? comment.Text;
            comment.UpdatedAt = DateTime.UtcNow;

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new InvalidDatabaseResultException();

            return mapper.Map<CommentDto>(comment);
        }
    }
}