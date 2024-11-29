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
    internal class DeleteCommandTest
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
        public async Task Delete_Returns_True()
        {
            // Arrange
            var postDto = new Fixture().Build<PostDto>().Create();
            var deletePostRequest = mapper.Map<DeletePostRequest>(postDto);
            var postDeletedRespond = new Fixture().Build<PostDeletedRespond>().With(x => x.CorrelationId, deletePostRequest.CorrelationId).With(x => x.Id, deletePostRequest.Id).Create();

            postRepositoryMock.Setup(x => x.Delete(postDto.Id)).ReturnsAsync(postDto);
            postRequestMock.Setup(x => x.Delete(deletePostRequest)).ReturnsAsync(postDeletedRespond);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.Delete(postDto.Id);

            // Assert
            act.Should().BeTrue();
        }
    }
}