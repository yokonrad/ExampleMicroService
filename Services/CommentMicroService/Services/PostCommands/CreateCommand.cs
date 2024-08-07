using AutoMapper;
using CommentMicroService.Data;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Exceptions;

namespace CommentMicroService.Services.PostCommands
{
    internal class CreateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<bool> Execute(PostDto postDto)
        {
            var post = mapper.Map<Post>(postDto);

            if (post is null) throw new MapperException();

            appDbContext.Posts.Add(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            return true;
        }
    }
}