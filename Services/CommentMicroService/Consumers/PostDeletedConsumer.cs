using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Services;
using MassTransit;
using Shared.Events;
using Shared.Exceptions;

namespace CommentMicroService.Consumers
{
    public class PostDeletedConsumer(PostService postService, ILogger<PostDeletedConsumer> logger, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<PostDeleted>
    {
        public async Task Consume(ConsumeContext<PostDeleted> postDeleted)
        {
            try
            {
                logger.LogInformation("Consuming PostDeleted with id: {Id}", postDeleted.Message.Id);

                var postDto = mapper.Map<PostDto>(postDeleted.Message);

                if (postDto is null) throw new MapperException();

                await postService.Delete(postDto.Id);
            }
            catch (Exception ex)
            {
                await publishEndpoint.Publish(new PostNotDeleted
                {
                    Id = postDeleted.Message.Id,
                    Message = ex.Message,
                });
            }
        }
    }
}