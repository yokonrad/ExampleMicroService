using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Microsoft.EntityFrameworkCore;
using Shared.Errors;

namespace CommentMicroService.Services.CommentCommands
{
    internal class GetByPostIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<List<CommentDto>> Execute(int postId)
        {
            var comments = await appDbContext.Comments.Include(x => x.Post).Where(x => x.PostId == postId).OrderBy(x => x.Id).ToListAsync();

            if (comments.Count == 0) throw new NotFoundError();

            return mapper.Map<List<CommentDto>>(comments);
        }
    }
}
