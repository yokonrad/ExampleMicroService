using MassTransit;
using Shared.Events;

namespace PostMicroService.Consumers
{
    public class PostNotDeletedConsumer(ILogger<PostNotDeletedConsumer> logger) : IConsumer<PostNotDeleted>
    {
        public async Task Consume(ConsumeContext<PostNotDeleted> postNotDeleted)
        {
            await Task.Run(() => logger.LogInformation("Consuming PostNotDeleted with id: {Id} and message: {Message}", postNotDeleted.Message.Id, postNotDeleted.Message.Message));
        }
    }
}