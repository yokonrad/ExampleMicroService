using MassTransit;

namespace Shared.Messages
{
    public record class PostNotDeletedMessage : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
        public string Message { get; init; }
    }
}