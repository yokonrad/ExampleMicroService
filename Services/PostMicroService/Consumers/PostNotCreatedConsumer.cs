using MassTransit;
using Shared.Events;

namespace PostMicroService.Consumers
{
    public class PostNotCreatedConsumer() : IConsumer<PostNotCreated>
    {
        public async Task Consume(ConsumeContext<PostNotCreated> postNotCreated)
        {
            Console.WriteLine($"Consuming PostNotCreated with id: {postNotCreated.Message.Id} and message: {postNotCreated.Message.Message}");
        }
    }
}