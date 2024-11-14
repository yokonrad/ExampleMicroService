namespace CommentMicroService.Dto
{
    public record class UpdateCommentDto
    {
        public int? PostId { get; init; }
        public string? Text { get; init; }
    }
}