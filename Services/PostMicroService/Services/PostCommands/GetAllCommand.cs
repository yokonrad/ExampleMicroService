using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Errors;

namespace PostMicroService.Services.PostCommands
{
    internal class GetAllCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<List<PostDto>> Execute()
        {
            var posts = await appDbContext.Posts.OrderBy(x => x.Id).ToListAsync();

            if (posts.Count == 0) throw new NotFoundError();

            return mapper.Map<List<PostDto>>(posts);
        }
    }
}
