using MassTransit;

namespace Shared.Requests
{
    public record class CreatePostRequest : CorrelatedBy<Guid>
    {
        public Guid CorrelationId { get; init; } = NewId.NextGuid();
        public int Id { get; init; }
    }
}