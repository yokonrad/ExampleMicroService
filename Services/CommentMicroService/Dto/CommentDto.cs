namespace CommentMicroService.Dto
{
    public record class CommentDto
    {
        public int Id { get; init; }
        public int PostId { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}