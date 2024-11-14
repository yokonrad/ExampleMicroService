using PostMicroService.Dto;

namespace PostMicroService.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAll();

        Task<PostDto> GetById(int id);

        Task<PostCommentDto> GetByIdComment(int id);

        Task<PostDto> Create(CreatePostDto createPostDto);

        Task<PostDto> Update(int id, UpdatePostDto updatePostDto);

        Task<bool> Delete(int id);
    }
}