using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Exceptions;

namespace CommentMicroService.Services.CommentCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(CreateCommentDto createCommentDto)
        {
            var post = await appDbContext.Posts.FindAsync(createCommentDto.PostId);

            if (post is null) throw new NotFoundException();

            var comment = mapper.Map<Comment>(createCommentDto);

            if (comment is null) throw new MapperException();

            appDbContext.Comments.Add(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            var commentDto = mapper.Map<CommentDto>(comment);

            if (commentDto is null) throw new MapperException();

            return commentDto;
        }
    }
}