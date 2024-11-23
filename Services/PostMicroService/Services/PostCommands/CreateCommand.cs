using AutoMapper;
using PostMicroService.Dto;
using PostMicroService.Repositories;
using PostMicroService.Requests;
using Shared.Requests;
using System.Transactions;

namespace PostMicroService.Services.PostCommands
{
    internal class CreateCommand(IPostRepository postRepository, IPostRequest postRequest, IMapper mapper)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            using var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var postDto = await postRepository.Create(createPostDto);

            var createPostRequest = mapper.Map<CreatePostRequest>(postDto);

            await postRequest.Create(createPostRequest);

            transaction.Complete();

            return postDto;
        }
    }
}