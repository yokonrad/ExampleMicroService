using AutoMapper;
using CommentMicroService.Dto;
using CommentMicroService.Entities;
using Shared.Requests;

namespace CommentMicroService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<CreatePostDto, Post>();

            CreateMap<CreatePostRequest, CreatePostDto>();
            CreateMap<DeletePostRequest, DeletePostDto>();

            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();

            CreateMap<CreateCommentDto, Comment>();
            CreateMap<UpdateCommentDto, Comment>();
        }
    }
}