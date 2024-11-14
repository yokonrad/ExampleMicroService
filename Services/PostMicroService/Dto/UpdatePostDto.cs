namespace PostMicroService.Dto
{
    public record class UpdatePostDto
    {
        public string? Title { get; init; }
        public bool? Visible { get; init; }
    }
}