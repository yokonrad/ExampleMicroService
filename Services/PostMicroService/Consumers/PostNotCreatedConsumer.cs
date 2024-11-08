using MassTransit;
using Shared.Events;

namespace PostMicroService.Consumers
{
    public class PostNotCreatedConsumer(ILogger<PostNotCreatedConsumer> logger) : IConsumer<PostNotCreated>
    {
        public async Task Consume(ConsumeContext<PostNotCreated> postNotCreated)
        {
            await Task.Run(() => logger.LogInformation("Consuming PostNotCreated with id: {Id} and message: {Message}", postNotCreated.Message.Id, postNotCreated.Message.Message));
        }
    }
}