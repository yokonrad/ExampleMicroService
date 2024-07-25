using CommentMicroService.Data;
using Shared.Errors;

namespace CommentMicroService.Services.PostCommands
{
    internal class DeleteCommand(AppDbContext appDbContext)
    {
        internal async Task<bool> Execute(int id)
        {
            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundError();

            appDbContext.Posts.Remove(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseError();

            return true;
        }
    }
}
