using CommentMicroService.Dto;

namespace CommentMicroService.Services
{
    public interface IPostService
    {
        Task<bool> Create(CreatePostDto createPostDto);
        Task<bool> Delete(int id);
    }
}