using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace CommentMicroService.Services.CommentCommands
{
    internal class GetByPostIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<IEnumerable<CommentDto>> Execute(int postId)
        {
            var comments = await appDbContext.Comments.Include(x => x.Post).Where(x => x.PostId == postId).OrderBy(x => x.Id).ToListAsync();

            if (comments.Count == 0) throw new NotFoundException();

            return mapper.Map<IEnumerable<CommentDto>>(comments);
        }
    }
}
