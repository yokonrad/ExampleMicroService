using System.ComponentModel.DataAnnotations;

namespace CommentMicroService.Dto
{
    public class CreateCommentDto
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Text { get; set; } = null!;
    }
}
