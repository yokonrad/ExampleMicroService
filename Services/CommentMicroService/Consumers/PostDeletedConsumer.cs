using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Services;
using MassTransit;
using Shared.Messages;
using Shared.Requests;

namespace CommentMicroService.Consumers
{
    public class PostDeletedConsumer(IPostService postService, ILogger<PostDeletedConsumer> logger, IMapper mapper, IPublishEndpoint publishEndpoint) : IConsumer<DeletePostRequest>
    {
        public async Task Consume(ConsumeContext<DeletePostRequest> deletePostRequest)
        {
            try
            {
                var deletePostDto = mapper.Map<DeletePostDto>(deletePostRequest.Message);

                logger.LogInformation("Consuming DeletePostRequest with id: {Id}", deletePostDto.Id);

                await postService.Delete(deletePostDto.Id);

                await publishEndpoint.Publish(new PostDeletedMessage
                {
                    CorrelationId = deletePostRequest.CorrelationId,
                    Id = deletePostRequest.Message.Id,
                });
            }
            catch (Exception ex)
            {
                await publishEndpoint.Publish(new PostNotDeletedMessage
                {
                    CorrelationId = deletePostRequest.CorrelationId,
                    Id = deletePostRequest.Message.Id,
                    Message = ex.Message,
                });
            }
        }
    }
}