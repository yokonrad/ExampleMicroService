using CommentMicroService.Dto;

namespace CommentMicroService.Services
{
    public interface ICommentService
    {
        Task<CommentDto> GetById(int id);

        Task<IEnumerable<CommentDto>> GetByPostId(int postId);

        Task<CommentDto> Create(CreateCommentDto createCommentDto);

        Task<CommentDto> Update(int id, UpdateCommentDto updateCommentDto);

        Task<bool> Delete(int id);
    }
}