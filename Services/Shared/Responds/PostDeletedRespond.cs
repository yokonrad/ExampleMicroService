using MassTransit;

namespace Shared.Responds
{
    public record class PostDeletedRespond : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
    }
}