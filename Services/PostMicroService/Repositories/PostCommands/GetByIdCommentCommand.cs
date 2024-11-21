using AutoMapper;
using Newtonsoft.Json;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Repositories.PostCommands
{
    internal class GetByIdCommentCommand(AppDbContext appDbContext, IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        internal async Task<PostCommentDto> Execute(int id)
        {
            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundException();

            var httpResponseMessage = await httpClientFactory.CreateClient().GetAsync($"{configuration["Services:CommentService"]}/api/v1/comment/{id}/post");

            if (!httpResponseMessage.IsSuccessStatusCode) throw new InvalidHttpResponseException();

            var postDto = mapper.Map<PostDto>(post);
            var commentsDto = JsonConvert.DeserializeObject<IEnumerable<CommentDto>>(await httpResponseMessage.Content.ReadAsStringAsync());

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