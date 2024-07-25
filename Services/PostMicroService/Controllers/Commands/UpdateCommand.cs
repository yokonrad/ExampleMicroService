using Microsoft.AspNetCore.Mvc;
using PostMicroService.Dto;
using PostMicroService.Services;
using Shared.Errors;

namespace PostMicroService.Controllers.Commands
{
    internal class UpdateCommand(PostController postController, PostService postService)
    {
        internal async Task<ActionResult> Execute(int id, UpdatePostDto updatePostDto)
        {
            try
            {
                return postController.Ok(await postService.Update(id, updatePostDto));
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
