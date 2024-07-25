using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Errors;

namespace CommentMicroService.Services.CommentCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(CreateCommentDto createCommentDto)
        {
            var post = await appDbContext.Posts.FindAsync(createCommentDto.PostId);

            if (post is null) throw new NotFoundError();

            var comment = mapper.Map<Comment>(createCommentDto);

            if (comment is null) throw new MapperError();

            appDbContext.Comments.Add(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseError();

            return mapper.Map<CommentDto>(comment);
        }
    }
}
