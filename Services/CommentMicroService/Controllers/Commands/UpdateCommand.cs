using CommentMicroService.Dto;
using CommentMicroService.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;

namespace CommentMicroService.Controllers.Commands
{
    internal class UpdateCommand(CommentController commentController, CommentService commentService)
    {
        internal async Task<ActionResult> Execute(int id, UpdateCommentDto updateCommentDto)
        {
            try
            {
                return commentController.Ok(await commentService.Update(id, updateCommentDto));
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
