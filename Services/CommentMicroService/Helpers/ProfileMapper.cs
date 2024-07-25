using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Events;

namespace CommentMicroService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<PostCreated, PostDto>();
            CreateMap<PostDeleted, PostDto>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}
