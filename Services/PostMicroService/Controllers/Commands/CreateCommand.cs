using Microsoft.AspNetCore.Mvc;
using PostMicroService.Dto;
using PostMicroService.Services;
using Shared.Errors;

namespace PostMicroService.Controllers.Commands
{
    internal class CreateCommand(PostController postController, PostService postService)
    {
        internal async Task<ActionResult> Execute(CreatePostDto createPostDto)
        {
            try
            {
                return postController.Ok(await postService.Create(createPostDto));
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
