using MassTransit;
using Shared.Messages;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.States
{
    public class PostStateMachine : MassTransitStateMachine<PostStateInstance>
    {
        public Event<CreatePostRequest> CreatePostEvent { get; set; }
        public Event<PostCreatedMessage> PostCreatedEvent { get; set; }
        public Event<PostNotCreatedMessage> PostNotCreatedEvent { get; set; }

        public Event<DeletePostRequest> DeletePostEvent { get; set; }
        public Event<PostDeletedMessage> PostDeletedEvent { get; set; }
        public Event<PostNotDeletedMessage> PostNotDeletedEvent { get; set; }

        public State CreatePost { get; set; }
        public State PostCreated { get; set; }
        public State PostNotCreated { get; set; }

        public State DeletePost { get; set; }
        public State PostDeleted { get; set; }
        public State PostNotDeleted { get; set; }

        public PostStateMachine()
        {
            InstanceState(x => x.CurrentState);

            Event(() => CreatePostEvent);
            Event(() => PostCreatedEvent);
            Event(() => PostNotCreatedEvent);

            Event(() => DeletePostEvent);
            Event(() => PostDeletedEvent);
            Event(() => PostNotDeletedEvent);

            Initially(
                When(CreatePostEvent)
                    .Then(x =>
                    {
                        x.Saga.RequestId = x.RequestId;
                        x.Saga.ResponseAddress = x.ResponseAddress!;
                        x.Saga.Id = x.Message.Id;
                    })
                    .TransitionTo(CreatePost),
                When(DeletePostEvent)
                    .Then(x =>
                    {
                        x.Saga.RequestId = x.RequestId;
                        x.Saga.ResponseAddress = x.ResponseAddress!;
                        x.Saga.Id = x.Message.Id;
                    })
                    .TransitionTo(DeletePost)
            );

            During(CreatePost,
                When(PostCreatedEvent)
                    .ThenAsync(async x =>
                    {
                        var sendEndpoint = await x.GetSendEndpoint(x.Saga.ResponseAddress);

                        await sendEndpoint.Send(new PostCreatedRespond
                        {
                            CorrelationId = x.Saga.CorrelationId,
                            Id = x.Message.Id,
                        }, r => r.RequestId = x.Saga.RequestId);
                    })
                    .TransitionTo(PostCreated),
                When(PostNotCreatedEvent)
                    .ThenAsync(async x =>
                    {
                        var sendEndpoint = await x.GetSendEndpoint(x.Saga.ResponseAddress);

                        await sendEndpoint.Send(new PostNotCreatedRespond
                        {
                            CorrelationId = x.Saga.CorrelationId,
                            Id = x.Message.Id,
                            Message = x.Message.Message,
                        }, r => r.RequestId = x.Saga.RequestId);
                    })
                    .TransitionTo(PostNotCreated)
            );

            During(DeletePost,
                When(PostDeletedEvent)
                    .ThenAsync(async x =>
                    {
                        var sendEndpoint = await x.GetSendEndpoint(x.Saga.ResponseAddress);

                        await sendEndpoint.Send(new PostDeletedRespond
                        {
                            CorrelationId = x.Message.CorrelationId,
                            Id = x.Message.Id,
                        }, r => r.RequestId = x.Saga.RequestId);
                    })
                    .TransitionTo(PostDeleted),
                When(PostNotDeletedEvent)
                    .ThenAsync(async x =>
                    {
                        var sendEndpoint = await x.GetSendEndpoint(x.Saga.ResponseAddress);

                        await sendEndpoint.Send(new PostNotDeletedRespond
                        {
                            CorrelationId = x.Message.CorrelationId,
                            Id = x.Message.Id,
                            Message = x.Message.Message,
                        }, r => r.RequestId = x.Saga.RequestId);
                    })
                    .TransitionTo(PostNotDeleted)
            );

            DuringAny(
                When(PostCreatedEvent)
                    .Finalize(),
                When(PostDeletedEvent)
                    .Finalize()
            );

            SetCompletedWhenFinalized();
        }
    }
}