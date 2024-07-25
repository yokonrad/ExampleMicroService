using CommentMicroService.Data;
using Shared.Errors;

namespace CommentMicroService.Services.CommentCommands
{
    internal class DeleteCommand(AppDbContext appDbContext)
    {
        internal async Task<bool> Execute(int id)
        {
            var comment = await appDbContext.Comments.FindAsync(id);

            if (comment is null) throw new NotFoundError();

            appDbContext.Comments.Remove(comment);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseError();

            return true;
        }
    }
}
