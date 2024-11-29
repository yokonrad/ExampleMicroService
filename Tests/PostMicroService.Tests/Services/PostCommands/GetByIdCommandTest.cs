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
    internal class GetByIdCommandTest
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
        public async Task GetById_Returns_NotNullObject()
        {
            var postDto = new Fixture().Build<PostDto>().Create();

            postRepositoryMock.Setup(x => x.GetById(postDto.Id)).ReturnsAsync(postDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.GetById(postDto.Id);

            // Assert
            act.Should().BeAssignableTo<PostDto>().And.NotBeNull().And.BeEquivalentTo(postDto);
        }
    }
}