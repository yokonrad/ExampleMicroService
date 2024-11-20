using MassTransit;

namespace Shared.Messages
{
    public record class PostCreatedMessage : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
    }
}