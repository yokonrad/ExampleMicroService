using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Services;
using MassTransit;
using Shared.Events;
using Shared.Exceptions;

namespace CommentMicroService.Consumers
{
    public class PostCreatedConsumer(PostService postService, ILogger<PostCreatedConsumer> logger, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<PostCreated>
    {
        public async Task Consume(ConsumeContext<PostCreated> postCreated)
        {
            try
            {
                logger.LogInformation("Consuming PostCreated with id: {Id}", postCreated.Message.Id);

                var postDto = mapper.Map<PostDto>(postCreated.Message);

                if (postDto is null) throw new MapperException();

                await postService.Create(postDto);
            }
            catch (Exception ex)
            {
                await publishEndpoint.Publish(new PostNotCreated
                {
                    Id = postCreated.Message.Id,
                    Message = ex.Message,
                });
            }
        }
    }
}