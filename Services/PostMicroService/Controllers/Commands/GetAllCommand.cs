using Microsoft.AspNetCore.Mvc;
using PostMicroService.Services;
using Shared.Errors;

namespace PostMicroService.Controllers.Commands
{
    internal class GetAllCommand(PostController postController, PostService postService)
    {
        internal async Task<ActionResult> Execute()
        {
            try
            {
                return postController.Ok(await postService.GetAll());
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
