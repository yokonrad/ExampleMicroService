using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.CommentCommands
{
    internal class GetByPostIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<IEnumerable<CommentDto>> Execute(int postId)
        {
            var post = await appDbContext.Posts.Include(p => p.Comments).FirstOrDefaultAsync(x => x.Id == postId);

            if (post is null) throw new NotFoundException();

            var comments = post.Comments;

            if (comments.Count == 0) return [];

            return mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }
}