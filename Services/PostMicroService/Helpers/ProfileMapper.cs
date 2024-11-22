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

            CreateMap<(PostDto postDto, CommentDto[] commentsDto), PostCommentDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.postDto.Id))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.postDto.Title))
                .ForMember(x => x.Visible, opt => opt.MapFrom(x => x.postDto.Visible))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => x.postDto.CreatedAt))
                .ForMember(x => x.UpdatedAt, opt => opt.MapFrom(x => x.postDto.UpdatedAt))
                .ForMember(x => x.Comments, opt => opt.MapFrom(x => x.commentsDto));

            CreateMap<CreatePostDto, Post>();
            CreateMap<UpdatePostDto, Post>();

            CreateMap<PostDto, CreatePostRequest>();
            CreateMap<PostDto, DeletePostRequest>();
        }
    }
}