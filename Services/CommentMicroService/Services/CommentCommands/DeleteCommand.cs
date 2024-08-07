using CommentMicroService.Data;
using Shared.Exceptions;

namespace CommentMicroService.Services.CommentCommands
{
    internal class DeleteCommand(AppDbContext appDbContext)
    {
        internal async Task<bool> Execute(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundException();

            appDbContext.Comments.Remove(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            return true;
        }
    }
}
