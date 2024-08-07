using System.ComponentModel.DataAnnotations;

namespace PostMicroService.Dto
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public bool Visible { get; set; }
    }
}