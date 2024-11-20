using MassTransit;

namespace Shared.Responds
{
    public record class PostNotDeletedRespond : CorrelatedBy<Guid?>
    {
        public Guid? CorrelationId { get; init; }
        public int Id { get; init; }
        public string Message { get; init; }
    }
}