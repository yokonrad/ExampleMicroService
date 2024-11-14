namespace PostMicroService.Dto
{
    public record class CreatePostDto
    {
        public required string Title { get; init; }
        public required bool Visible { get; init; }
    }
}