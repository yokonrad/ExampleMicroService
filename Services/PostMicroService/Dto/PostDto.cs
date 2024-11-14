namespace PostMicroService.Dto
{
    public record class PostDto
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public bool Visible { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime UpdatedAt { get; init; }
    }
}