using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Microsoft.EntityFrameworkCore;
using Shared.Exceptions;

namespace CommentMicroService.Services.CommentCommands
{
    internal class GetByIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(int id)
        {
            var comment = await appDbContext.Comments.Include(x => x.Post).FirstOrDefaultAsync(x => x.Id == id);

            if (comment is null) throw new NotFoundException();

            return mapper.Map<CommentDto>(comment);
        }
    }
}