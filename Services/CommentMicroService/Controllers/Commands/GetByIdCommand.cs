using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace CommentMicroService.Controllers.Commands
{
    internal class GetByIdCommand(CommentController commentController, CommentService commentService)
    {
        internal async Task<ActionResult> Execute(int id)
        {
            try
            {
                return commentController.Ok(await commentService.GetById(id));
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
