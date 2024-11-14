using AutoMapper;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Repositories.PostCommands
{
    internal class DeleteCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<PostDto> Execute(int id)
        {
            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundException();

            appDbContext.Posts.Remove(post);

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            return mapper.Map<PostDto>(post);
        }
    }
}