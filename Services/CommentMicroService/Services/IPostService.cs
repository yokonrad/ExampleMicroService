using CommentMicroService.Dto;

namespace CommentMicroService.Services
{
    public interface IPostService
    {
        Task<PostDto> Create(CreatePostDto createPostDto);

        Task<bool> Delete(int id);
    }
}