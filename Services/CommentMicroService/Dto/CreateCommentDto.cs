namespace CommentMicroService.Dto
{
    public record class CreateCommentDto
    {
        public required int PostId { get; init; }
        public required string Text { get; init; }
    }
}