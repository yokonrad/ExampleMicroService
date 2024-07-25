using Microsoft.AspNetCore.Mvc;
using PostMicroService.Services;
using Shared.Errors;

namespace PostMicroService.Controllers.Commands
{
    internal class DeleteCommand(PostController postController, PostService postService)
    {
        internal async Task<ActionResult> Execute(int id)
        {
            try
            {
                return postController.Ok(await postService.Delete(id));
            }
            catch (NotFoundError ex)
            {
                return postController.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return postController.Problem(ex.Message);
            }
        }
    }
}
