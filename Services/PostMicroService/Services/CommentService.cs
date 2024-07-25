using PostMicroService.Dto;
using PostMicroService.Services.CommentCommands;

namespace PostMicroService.Services
{
    public class CommentService(PostService postService, HttpClient httpClient, IConfiguration configuration)
    {
        private readonly GetByIdCommentCommand _getByIdCommentCommand = new(postService, httpClient, configuration);

        public async Task<PostCommentDto?> GetByIdComment(int id) => await _getByIdCommentCommand.Execute(id);
    }
}
