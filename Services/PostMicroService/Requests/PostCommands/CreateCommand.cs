using MassTransit;
using Shared.Exceptions;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Requests.PostCommands
{
    internal class CreateCommand(IRequestClient<CreatePostRequest> createPostRequestClient)
    {
        internal async Task<PostCreatedRespond> Execute(CreatePostRequest createPostRequest)
        {
            var response = await createPostRequestClient.GetResponse<PostCreatedRespond, PostNotCreatedRespond>(createPostRequest);

            if (!response.Is(out Response<PostCreatedRespond>? postCreatedRespond)) throw new InvalidResponseException();

            return postCreatedRespond.Message;
        }
    }
}