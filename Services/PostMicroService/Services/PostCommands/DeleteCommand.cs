using AutoMapper;
using MassTransit;
using PostMicroService.Repositories;
using Shared.Events;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class DeleteCommand(IPostRepository postRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        internal async Task<bool> Execute(int id)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Delete(id);
            var postDeleted = mapper.Map<PostDeleted>(postDto);

            await publishEndpoint.Publish(postDeleted);

            transaction.Complete();

            return true;
        }
    }
}