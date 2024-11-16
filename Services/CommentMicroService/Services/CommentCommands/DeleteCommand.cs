using CommentMicroService.Repositories;

namespace CommentMicroService.Services.CommentCommands
{
    internal class DeleteCommand(ICommentRepository commentRepository)
    {
        internal async Task<bool> Execute(int id)
        {
            await commentRepository.Delete(id);

            return true;
        }
    }
}