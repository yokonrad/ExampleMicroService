using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Services;
using MassTransit;
using Shared.Messages;
using Shared.Requests;

namespace CommentMicroService.Consumers
{
    public class PostCreatedConsumer(IPostService postService, ILogger<PostCreatedConsumer> logger, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<CreatePostRequest>
    {
        public async Task Consume(ConsumeContext<CreatePostRequest> createPostRequest)
        {
            try
            {
                var createPostDto = mapper.Map<CreatePostDto>(createPostRequest.Message);

                logger.LogInformation("Consuming CreatePostRequest with id: {Id}", createPostDto.Id);

                await postService.Create(createPostDto);

                await publishEndpoint.Publish(new PostCreatedMessage
                {
                    CorrelationId = createPostRequest.CorrelationId,
                    Id = createPostRequest.Message.Id,
                });
            }
            catch (Exception ex)
            {
                await publishEndpoint.Publish(new PostNotCreatedMessage
                {
                    CorrelationId = createPostRequest.CorrelationId,
                    Id = createPostRequest.Message.Id,
                    Message = ex.Message,
                });
            }
        }
    }
}