using MassTransit;

namespace Shared.Messages
{
    public record class PostDeletedMessage : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
    }
}