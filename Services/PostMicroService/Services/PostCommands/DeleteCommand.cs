using AutoMapper;
using MassTransit;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Events;
using Shared.Exceptions;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class DeleteCommand(AppDbContext appDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        internal async Task<bool> Execute(int id)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundException();

            appDbContext.Posts.Remove(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            var postDto = mapper.Map<PostDto>(post);

            if (postDto is null) throw new MapperException();

            await publishEndpoint.Publish(mapper.Map<PostDeleted>(postDto));

            transaction.Complete();

            return true;
        }
    }
}
