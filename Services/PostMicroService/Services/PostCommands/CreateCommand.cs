using AutoMapper;
using MassTransit;
using PostMicroService.Dto;
using PostMicroService.Repositories;
using Shared.Events;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class CreateCommand(IPostRepository postRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Create(createPostDto);
            var postCreated = mapper.Map<PostCreated>(postDto);

            await publishEndpoint.Publish(postCreated);

            transaction.Complete();

            return postDto;
        }
    }
}