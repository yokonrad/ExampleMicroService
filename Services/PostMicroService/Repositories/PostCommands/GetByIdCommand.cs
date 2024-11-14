using AutoMapper;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Repositories.PostCommands
{
    internal class GetByIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<PostDto> Execute(int id)
        {
            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundException();

            return mapper.Map<PostDto>(post);
        }
    }
}