namespace CommentMicroService.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public virtual ICollection<Comment> Comments { get; set; } = null!;
    }
}