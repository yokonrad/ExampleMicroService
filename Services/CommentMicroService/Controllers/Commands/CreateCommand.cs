using CommentMicroService.Dto;
using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace CommentMicroService.Controllers.Commands
{
    internal class CreateCommand(CommentController commentController, CommentService commentService)
    {
        internal async Task<ActionResult> Execute(CreateCommentDto createCommentDto)
        {
            try
            {
                return commentController.Ok(await commentService.Create(createCommentDto));
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
