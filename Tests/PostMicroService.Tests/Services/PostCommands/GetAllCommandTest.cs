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
    internal class GetAllCommandTest
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
        public async Task GetAll_Returns_EmptyCollection()
        {
            // Arrange
            IEnumerable<PostDto> postsDto = [];

            postRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(postsDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.GetAll();

            // Assert
            act.Should().BeAssignableTo<IEnumerable<PostDto>>().And.BeEmpty().And.BeEquivalentTo(postsDto);
        }

        [Test]
        public async Task GetAll_Returns_NotEmptyCollection()
        {
            // Arrange
            IEnumerable<PostDto> postsDto = new Fixture().Build<PostDto>().CreateMany(10).ToArray();

            postRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(postsDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.GetAll();

            // Assert
            act.Should().BeAssignableTo<IEnumerable<PostDto>>().And.NotBeEmpty().And.BeEquivalentTo(postsDto);
        }
    }
}