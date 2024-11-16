using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Exceptions;

namespace CommentMicroService.Repositories.PostCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<PostDto> Execute(CreatePostDto createPostDto)
        {
            var post = mapper.Map<Post>(createPostDto);

            appDbContext.Posts.Add(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            return mapper.Map<PostDto>(post);
        }
    }
}