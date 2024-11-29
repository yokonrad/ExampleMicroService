using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using PostMicroService.Dto;
using PostMicroService.Helpers;
using PostMicroService.Repositories;
using PostMicroService.Requests;
using PostMicroService.Services;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Tests.Services.PostCommands
{
    internal class CreateCommandTest
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
        public async Task Create_Returns_NotNullObject()
        {
            // Arrange
            var postDto = new Fixture().Build<PostDto>().Create();
            var createPostDto = new Fixture().Build<CreatePostDto>().With(x => x.Title, postDto.Title).With(x => x.Visible, postDto.Visible).Create();
            var createPostRequest = mapper.Map<CreatePostRequest>(postDto);
            var postCreatedRespond = new Fixture().Build<PostCreatedRespond>().With(x => x.CorrelationId, createPostRequest.CorrelationId).With(x => x.Id, createPostRequest.Id).Create();

            postRepositoryMock.Setup(x => x.Create(createPostDto)).ReturnsAsync(postDto);
            postRequestMock.Setup(x => x.Create(createPostRequest)).ReturnsAsync(postCreatedRespond);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.Create(createPostDto);

            // Assert
            act.Should().BeOfType<PostDto>().And.NotBeNull().And.BeEquivalentTo(postDto);
        }
    }
}