using CommentMicroService.Dto;

namespace CommentMicroService.Repositories
{
    public interface IPostRepository
    {
        Task<PostDto> Create(CreatePostDto createPostDto);

        Task<PostDto> Delete(int id);
    }
}