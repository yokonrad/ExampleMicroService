using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using PostMicroService.Dto;
using PostMicroService.Helpers;
using PostMicroService.Repositories;
using PostMicroService.Requests;
using PostMicroService.Services;

namespace PostMicroService.Tests.Services.PostCommands
{
    internal class GetByIdCommentCommandTest
    {
        private Mock<IPostRepository> postRepositoryMock;
        private Mock<IPostRequest> postRequestMock;
        private Mapper mapper;

        [SetUp]
        public async Task SetUp()
        {
            postRepositoryMock = new Mock<IPostRepository>();

            postRequestMock = new Mock<IPostRequest>();

            mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new ProfileMapper())));
        }

        [Test]
        public async Task GetByIdComment_Returns_NotNullObject_WithoutComments()
        {
            var postDto = new Fixture().Build<PostDto>().Create();
            var commentsDto = Array.Empty<CommentDto>();
            var postCommentDto = mapper.Map<PostCommentDto>((postDto, commentsDto));

            postRepositoryMock.Setup(x => x.GetByIdComment(postDto.Id)).ReturnsAsync(postCommentDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.GetByIdComment(postDto.Id);

            // Assert
            act.Should().BeAssignableTo<PostCommentDto>().And.NotBeNull().And.BeEquivalentTo(postCommentDto);
        }

        [Test]
        public async Task GetByIdComment_Returns_NotNullObject_WithComments()
        {
            var postDto = new Fixture().Build<PostDto>().Create();
            var commentsDto = new Fixture().Build<CommentDto>().CreateMany(10).ToArray();
            var postCommentDto = mapper.Map<PostCommentDto>((postDto, commentsDto));

            postRepositoryMock.Setup(x => x.GetByIdComment(postDto.Id)).ReturnsAsync(postCommentDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.GetByIdComment(postDto.Id);

            // Assert
            act.Should().BeAssignableTo<PostCommentDto>().And.NotBeNull().And.BeEquivalentTo(postCommentDto);
        }
    }
}