using AutoMapper;
using MassTransit;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Services.PostCommands;

namespace PostMicroService.Services
{
    public class PostService
    {
        private readonly GetAllCommand _getAllCommand;
        private readonly GetByIdCommand _getByIdCommand;
        private readonly GetByIdCommentCommand _getByIdCommentCommand;
        private readonly CreateCommand _createCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly DeleteCommand _deleteCommand;

        public PostService(AppDbContext appDbContext, IMapper mapper, HttpClient httpClient, IConfiguration configuration, IPublishEndpoint publishEndpoint)
        {
            _getAllCommand = new(appDbContext, mapper);
            _getByIdCommand = new(appDbContext, mapper);
            _getByIdCommentCommand = new(this, httpClient, configuration);
            _createCommand = new(appDbContext, mapper, publishEndpoint);
            _updateCommand = new(appDbContext, mapper);
            _deleteCommand = new(appDbContext, mapper, publishEndpoint);
        }

        public async Task<IEnumerable<PostDto>> GetAll() => await _getAllCommand.Execute();

        public async Task<PostDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<PostCommentDto> GetByIdComment(int id) => await _getByIdCommentCommand.Execute(id);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<PostDto> Update(int id, UpdatePostDto updatePostDto) => await _updateCommand.Execute(id, updatePostDto);

        public async Task<bool> Delete(int id) => await _deleteCommand.Execute(id);
    }
}