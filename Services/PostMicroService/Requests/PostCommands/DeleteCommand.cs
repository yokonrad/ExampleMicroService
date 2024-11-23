using MassTransit;
using Shared.Exceptions;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Requests.PostCommands
{
    internal class DeleteCommand(IRequestClient<DeletePostRequest> deletePostRequestClient)
    {
        internal async Task<PostDeletedRespond> Execute(DeletePostRequest deletePostRequest)
        {
            var response = await deletePostRequestClient.GetResponse<PostDeletedRespond, PostNotDeletedRespond>(deletePostRequest);

            if (!response.Is(out Response<PostDeletedRespond>? postDeletedRespond)) throw new InvalidResponseException();

            return postDeletedRespond.Message;
        }
    }
}