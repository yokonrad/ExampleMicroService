using AutoMapper;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Repositories.PostCommands;

namespace PostMicroService.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly GetAllCommand _getAllCommand;
        private readonly GetByIdCommand _getByIdCommand;
        private readonly GetByIdCommentCommand _getByIdCommentCommand;
        private readonly CreateCommand _createCommand;
        private readonly UpdateCommand _updateCommand;
        private readonly DeleteCommand _deleteCommand;

        public PostRepository(AppDbContext appDbContext, IMapper mapper, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _getAllCommand = new(appDbContext, mapper);
            _getByIdCommand = new(appDbContext, mapper);
            _getByIdCommentCommand = new(appDbContext, mapper, httpClientFactory, configuration);
            _createCommand = new(appDbContext, mapper);
            _updateCommand = new(appDbContext, mapper);
            _deleteCommand = new(appDbContext, mapper);
        }

        public async Task<IEnumerable<PostDto>> GetAll() => await _getAllCommand.Execute();

        public async Task<PostDto> GetById(int id) => await _getByIdCommand.Execute(id);

        public async Task<PostCommentDto> GetByIdComment(int id) => await _getByIdCommentCommand.Execute(id);

        public async Task<PostDto> Create(CreatePostDto createPostDto) => await _createCommand.Execute(createPostDto);

        public async Task<PostDto> Update(int id, UpdatePostDto updatePostDto) => await _updateCommand.Execute(id, updatePostDto);

        public async Task<PostDto> Delete(int id) => await _deleteCommand.Execute(id);
    }
}