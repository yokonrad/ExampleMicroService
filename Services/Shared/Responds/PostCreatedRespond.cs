using MassTransit;

namespace Shared.Responds
{
    public record class PostCreatedRespond : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
    }
}