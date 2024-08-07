namespace PostMicroService.Dto
{
    public class PostCommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool Visible { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<CommentDto> Comments { get; set; } = null!;
    }
}