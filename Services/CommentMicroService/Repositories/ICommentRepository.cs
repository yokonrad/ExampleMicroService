using CommentMicroService.Dto;

namespace CommentMicroService.Repositories
{
    public interface ICommentRepository
    {
        Task<CommentDto> GetById(int id);

        Task<IEnumerable<CommentDto>> GetByPostId(int postId);

        Task<CommentDto> Create(CreateCommentDto createCommentDto);

        Task<CommentDto> Update(int id, UpdateCommentDto updateCommentDto);

        Task<CommentDto> Delete(int id);
    }
}