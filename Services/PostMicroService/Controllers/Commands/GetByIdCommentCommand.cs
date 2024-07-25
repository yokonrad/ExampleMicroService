using Microsoft.AspNetCore.Mvc;
using PostMicroService.Services;
using Shared.Errors;

namespace PostMicroService.Controllers.Commands
{
    internal class GetByIdCommentCommand(PostController postController, CommentService commentService)
    {
        internal async Task<ActionResult> Execute(int id)
        {
            try
            {
                return postController.Ok(await commentService.GetByIdComment(id));
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
