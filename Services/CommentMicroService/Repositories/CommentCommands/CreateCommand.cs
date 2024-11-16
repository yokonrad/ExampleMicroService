using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.CommentCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(CreateCommentDto createCommentDto)
        {
            var post = await appDbContext.Posts.FindAsync(createCommentDto.PostId);

            if (post is null) throw new NotFoundException();

            var comment = mapper.Map<Comment>(createCommentDto);

            appDbContext.Comments.Add(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            return mapper.Map<CommentDto>(comment);
        }
    }
}