namespace PostMicroService.Dto
{
    public record class PostCommentDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public bool Visible { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
        public IEnumerable<CommentDto> Comments { get; init; }
    }
}