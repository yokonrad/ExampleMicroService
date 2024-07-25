using CommentMicroService.Entities;

namespace CommentMicroService.Dto
{
    public class PostDto
    {
        public int Id { get; set; }
        public ICollection<Comment> Comments { get; set; } = null!;
    }
}
