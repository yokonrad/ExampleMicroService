using MassTransit;
using PostMicroService.Requests.PostCommands;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Requests
{
    public class PostRequest : IPostRequest
    {
        private readonly CreateCommand _createCommand;
        private readonly DeleteCommand _deleteCommand;

        public PostRequest(IRequestClient<CreatePostRequest> createPostRequestClient, IRequestClient<DeletePostRequest> deletePostRequestClient)
        {
            _createCommand = new(createPostRequestClient);
            _deleteCommand = new(deletePostRequestClient);
        }

        public async Task<PostCreatedRespond> Create(CreatePostRequest createPostRequest) => await _createCommand.Execute(createPostRequest);

        public async Task<PostDeletedRespond> Delete(DeletePostRequest deletePostRequest) => await _deleteCommand.Execute(deletePostRequest);
    }
}