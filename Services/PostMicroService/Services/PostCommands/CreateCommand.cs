using AutoMapper;
using MassTransit;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Entities;
using Shared.Errors;
using Shared.Events;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var post = mapper.Map<Post>(createPostDto);

            if (post is null) throw new MapperError();

            appDbContext.Posts.Add(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseError();

            var postDto = mapper.Map<PostDto>(post);

            if (postDto is null) throw new MapperError();

            await publishEndpoint.Publish(mapper.Map<PostCreated>(postDto));

            transaction.Complete();

            return postDto;
        }
    }
}
