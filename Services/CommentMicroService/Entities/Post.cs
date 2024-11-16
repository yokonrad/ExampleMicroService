namespace CommentMicroService.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public ICollection<Comment> Comments { get; } = [];
    }
}