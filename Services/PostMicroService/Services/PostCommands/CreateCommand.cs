using AutoMapper;
using MassTransit;
using PostMicroService.Dto;
using PostMicroService.Repositories;
using Shared.Exceptions;
using Shared.Requests;
using Shared.Responds;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class CreateCommand(IPostRepository postRepository, IMapper mapper, IRequestClient<CreatePostRequest> createPostRequestClient)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Create(createPostDto);

            var createPostRequest = mapper.Map<CreatePostRequest>(postDto);

            var response = await createPostRequestClient.GetResponse<PostCreatedRespond, PostNotCreatedRespond>(createPostRequest);

            if (response.Is(out Response<PostNotCreatedRespond>? _)) throw new InvalidResponseException();

            transaction.Complete();

            return postDto;
        }
    }
}