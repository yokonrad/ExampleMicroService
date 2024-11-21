using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Data;
using PostMicroService.Dto;

namespace PostMicroService.Repositories.PostCommands
{
    internal class GetAllCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<IEnumerable<PostDto>> Execute()
        {
            var posts = await appDbContext.Posts.OrderBy(x => x.Id).ToListAsync();

            if (posts.Count == 0) return [];

            return mapper.Map<PostDto[]>(posts.ToArray());
        }
    }
}