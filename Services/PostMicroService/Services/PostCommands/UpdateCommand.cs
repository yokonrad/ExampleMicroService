﻿using AutoMapper;
using PostMicroService.Data;
using PostMicroService.Dto;
using Shared.Exceptions;

namespace PostMicroService.Services.PostCommands
{
    internal class UpdateCommand(AppDbContext appDbContext, IMapper mapper)
    {
        internal async Task<PostDto> Execute(int id, UpdatePostDto updatePostDto)
        {
            var post = await appDbContext.Posts.FindAsync(id);

            if (post is null) throw new NotFoundException();

            post.Title = updatePostDto.Title ?? post.Title;
            post.Visible = updatePostDto.Visible ?? post.Visible;
            post.UpdatedAt = DateTime.UtcNow;

            var result = await appDbContext.SaveChangesAsync() > 0;

            if (!result) throw new DatabaseException();

            var postDto = mapper.Map<PostDto>(post);

            if (postDto is null) throw new MapperException();

            return postDto;
        }
    }
}