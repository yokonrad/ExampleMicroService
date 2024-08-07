using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Shared.Exceptions;

namespace CommentMicroService.Services.CommentCommands
{
    internal class UpdateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(int id, UpdateCommentDto updateCommentDto)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundException();

            if (updateCommentDto.PostId is not null)
            {
                var post = await appDbContext.Posts.FindAsync(updateCommentDto.PostId);

                if (post is null) throw new NotFoundException();
            }

            comment.Text = updateCommentDto.Text ?? comment.Text;
            comment.PostId = updateCommentDto.PostId ?? comment.PostId;
            comment.UpdatedAt = DateTime.UtcNow;

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            var commentDto = mapper.Map<CommentDto>(comment);

            if (commentDto is null) throw new MapperException();

            return commentDto;
        }
    }
}