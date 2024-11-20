using MassTransit;

namespace PostMicroService.States
{
    public class PostStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public Uri ResponseAddress { get; set; }
        public int Id { get; set; }
    }
}