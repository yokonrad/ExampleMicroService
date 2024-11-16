using CommentMicroService.Repositories;

namespace CommentMicroService.Services.PostCommands
{
    internal class DeleteCommand(IPostRepository postRepository)
    {
        internal async Task<bool> Execute(int id)
        {
            await postRepository.Delete(id);

            return true;
        }
    }
}