using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.CommentCommands
{
    internal class DeleteCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<CommentDto> Execute(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundException();

            appDbContext.Comments.Remove(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new InvalidDatabaseResultException();

            return mapper.Map<CommentDto>(comment);
        }
    }
}