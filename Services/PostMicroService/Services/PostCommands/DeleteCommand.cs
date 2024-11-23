using AutoMapper;
using PostMicroService.Repositories;
using PostMicroService.Requests;
using Shared.Requests;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class DeleteCommand(IPostRepository postRepository, IPostRequest postRequest, IMapper mapper)
    {
        internal async Task<bool> Execute(int id)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Delete(id);

            var deletePostRequest = mapper.Map<DeletePostRequest>(postDto);

            await postRequest.Delete(deletePostRequest);

            transaction.Complete();

            return true;
        }
    }
}