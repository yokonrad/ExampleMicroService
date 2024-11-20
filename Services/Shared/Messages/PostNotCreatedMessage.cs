using MassTransit;

namespace Shared.Messages
{
    public record class PostNotCreatedMessage : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
        public string Message { get; init; }
    }
}