using AutoMapper;
using MassTransit;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Services.PostCommands;

namespace PostMicroService.Services
{
    public class PostService(AppDbContext appDbContext, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        private readonly GetAllCommand _getAllCommand = new(appDbContext, mapper);
        private readonly GetByIdCommand _getByIdCommand = new(appDbContext, mapper);
        private readonly CreateCommand _createCommand = new(appDbContext, mapper, publishEndpoint);
        private readonly UpdateCommand _updateCommand = new(appDbContext, mapper);
        private readonly DeleteCommand _deleteCommand = new(appDbContext, mapper, publishEndpoint);

        public async Task<IEnumerable<PostDto>> GetAll() => await _getAllCommand.Execute();

        public async Task<PostDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<PostDto> Update(int id, UpdatePostDto updatePostDto) => await _updateCommand.Execute(id, updatePostDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}