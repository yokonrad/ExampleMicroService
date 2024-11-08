using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Services.PostCommands
{
    internal class GetAllCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<IEnumerable<PostDto>> Execute()
        {
            var posts = await appDbContext.Posts.OrderBy(x => x.Id).ToListAsync();

            if (posts.Count == 0) throw new NotFoundException();

            var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);

            if (postsDto is null) throw new MapperException();

            return postsDto;
        }
    }
}