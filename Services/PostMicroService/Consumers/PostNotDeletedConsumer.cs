using MassTransit;
using Shared.Events;

namespace PostMicroService.Consumers
{
    public class PostNotDeletedConsumer() : IConsumer<PostNotDeleted>
    {
        public async Task Consume(ConsumeContext<PostNotDeleted> postNotDeleted)
        {
            await Task.Run(() => Console.WriteLine($"Consuming PostNotDeleted with id: {postNotDeleted.Message.Id} and message: {postNotDeleted.Message.Message}"));
        }
    }
}