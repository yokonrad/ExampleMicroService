using AutoMapper;
using MassTransit;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Entities;
using Shared.Events;
using Shared.Exceptions;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var post = mapper.Map<Post>(createPostDto);

            if (post is null) throw new MapperException();

            appDbContext.Posts.Add(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            var postDto = mapper.Map<PostDto>(post);

            if (postDto is null) throw new MapperException();

            await publishEndpoint.Publish(mapper.Map<PostCreated>(postDto));

            transaction.Complete();

            return postDto;
        }
    }
}
