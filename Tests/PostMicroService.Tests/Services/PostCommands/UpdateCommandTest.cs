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
    internal class UpdateCommandTest
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
        public async Task Update_Returns_NotNullObject()
        {
            // Arrange
            var postDto = new Fixture().Build<PostDto>().Create();
            var updatePostDto = new Fixture().Build<UpdatePostDto>().With(x => x.Title, postDto.Title).With(x => x.Visible, postDto.Visible).Create();

            postRepositoryMock.Setup(x => x.Update(postDto.Id, updatePostDto)).ReturnsAsync(postDto);

            var postService = new PostService(postRepositoryMock.Object, postRequestMock.Object, mapper);

            // Act
            var act = await postService.Update(postDto.Id, updatePostDto);

            // Assert
            act.Should().BeOfType<PostDto>().And.NotBeNull().And.BeEquivalentTo(postDto);
        }
    }
}