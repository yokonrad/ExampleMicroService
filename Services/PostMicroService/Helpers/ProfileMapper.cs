using AutoMapper;
using PostMicroService.Dto;
using PostMicroService.Entities;
using Shared.Requests;

namespace PostMicroService.Helpers
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();

            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();

            CreateMap<PostDto, CreatePostRequest>();
            CreateMap<PostDto, DeletePostRequest>();
        }
    }
}