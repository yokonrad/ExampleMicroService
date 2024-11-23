using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Requests
{
    public interface IPostRequest
    {
        Task<PostCreatedRespond> Create(CreatePostRequest createPostRequest);

        Task<PostDeletedRespond> Delete(DeletePostRequest deletePostRequest);
    }
}