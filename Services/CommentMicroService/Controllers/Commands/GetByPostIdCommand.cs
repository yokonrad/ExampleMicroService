using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace CommentMicroService.Controllers.Commands
{
    internal class GetByPostIdCommand(CommentController commentController, CommentService commentService)
    {
        internal async Task<ActionResult> Execute(int postId)
        {
            try
            {
                return commentController.Ok(await commentService.GetByPostId(postId));
            }
            catch (NotFoundError ex)
            {
                return commentController.NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return commentController.Problem(ex.Message);
            }
        }
    }
}
