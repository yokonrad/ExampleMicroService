﻿using AutoMapper;
using PostMicroService.Dto;
using PostMicroService.Repositories;
using PostMicroService.Requests;
using PostMicroService.Services.PostCommands;

namespace PostMicroService.Services
{
    public class PostService : IPostService
    {
        private readonly GetAllCommand _getAllCommand;
        private readonly GetByIdCommand _getByIdCommand;
        private readonly GetByIdCommentCommand _getByIdCommentCommand;
        private readonly CreateCommand _createCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly DeleteCommand _deleteCommand;

        public PostService(IPostRepository postRepository, IPostRequest postRequest, IMapper mapper)
        {
            _getAllCommand = new(postRepository);
            _getByIdCommand = new(postRepository);
            _getByIdCommentCommand = new(postRepository);
            _createCommand = new(postRepository, postRequest, mapper);
            _updateCommand = new(postRepository);
            _deleteCommand = new(postRepository, postRequest, mapper);
        }

        public async Task<IEnumerable<PostDto>> GetAll() => await _getAllCommand.Execute();

        public async Task<PostDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<PostCommentDto> GetByIdComment(int id) => await _getByIdCommentCommand.Execute(id);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<PostDto> Update(int id, UpdatePostDto updatePostDto) => await _updateCommand.Execute(id, updatePostDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}