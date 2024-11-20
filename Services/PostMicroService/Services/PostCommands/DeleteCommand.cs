using AutoMapper;
using MassTransit;
using PostMicroService.Repositories;
using Shared.Exceptions;
using Shared.Requests;
using Shared.Responds;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class DeleteCommand(IPostRepository postRepository, IMapper mapper, IRequestClient<DeletePostRequest> deletePostRequestClient)
    {
        internal async Task<bool> Execute(int id)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Delete(id);

            var deletePostRequest = mapper.Map<DeletePostRequest>(postDto);

            var response = await deletePostRequestClient.GetResponse<PostDeletedRespond, PostNotDeletedRespond>(deletePostRequest);

            if (response.Is(out Response<PostNotDeletedRespond>? _)) throw new InvalidResponseException();

            transaction.Complete();

            return true;
        }
    }
}