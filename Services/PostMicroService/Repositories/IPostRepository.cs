using PostMicroService.Dto;

namespace PostMicroService.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostDto>> GetAll();

        Task<PostDto> GetById(int id);

        Task<PostCommentDto> GetByIdComment(int id);

        Task<PostDto> Create(CreatePostDto createPostDto);

        Task<PostDto> Update(int id, UpdatePostDto updatePostDto);

        Task<PostDto> Delete(int id);
    }
}