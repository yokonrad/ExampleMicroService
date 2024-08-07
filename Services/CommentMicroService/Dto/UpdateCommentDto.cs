namespace CommentMicroService.Dto
{
    public class UpdateCommentDto
    {
        public int? PostId { get; set; }
        public string? Text { get; set; }
    }
}