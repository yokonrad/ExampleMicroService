using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Services.PostCommands
{
    internal class GetByIdCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<PostDto> Execute(int id)
        {
            var post = await appDbContext.Posts.FirstOrDefaultAsync(x => x.Id == id);

            if (post is null) throw new NotFoundException();

            return mapper.Map<PostDto>(post);
        }
    }
}