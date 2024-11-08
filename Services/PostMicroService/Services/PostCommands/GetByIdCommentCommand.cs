using Newtonsoft.Json;
using PostMicroService.Dto;

namespace PostMicroService.Services.PostCommands
{
    internal class GetByIdCommentCommand(PostService postService, HttpClient httpClient, IConfiguration configuration)
    {
        internal async Task<PostCommentDto> Execute(int id)
        {
            var postDto = await postService.GetById(id);

            var commentsDto = new List<CommentDto>();

            var httpResponseMessage = await httpClient.GetAsync($"{configuration["Services:CommentService"]}/api/v1/comment/{id}/post");

            if (httpResponseMessage.IsSuccessStatusCode) commentsDto = JsonConvert.DeserializeObject<List<CommentDto>>(await httpResponseMessage.Content.ReadAsStringAsync());

            return new PostCommentDto
            {
                Id = postDto.Id,
                Title = postDto.Title,
                Visible = postDto.Visible,
                CreatedAt = postDto.CreatedAt,
                UpdatedAt = postDto.UpdatedAt,
                Comments = commentsDto ?? [],
            };
        }
    }
}