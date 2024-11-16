namespace CommentMicroService.Dto
{
    public record class UpdateCommentDto
    {
        public string? Text { get; init; }
    }
}