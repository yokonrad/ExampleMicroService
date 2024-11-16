using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.CommentCommands
{
    internal class GetByIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundException();

            return mapper.Map<CommentDto>(comment);
        }
    }
}