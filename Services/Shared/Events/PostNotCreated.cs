namespace Shared.Events
{
    public class PostNotCreated
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
    }
}