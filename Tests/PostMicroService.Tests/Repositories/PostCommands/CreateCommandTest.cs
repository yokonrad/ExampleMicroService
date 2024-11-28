using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Entities;
using PostMicroService.Helpers;
using PostMicroService.Repositories;
using Shared.Exceptions;

namespace PostMicroService.Tests.Repositories.PostCommands
{
    internal class CreateCommandTest
    {
        private Mock<AppDbContext> appDbContextMock;
        private Mapper mapper;
        private Mock<IHttpClientFactory> httpClientFactoryMock;
        private Mock<IConfiguration> configurationMock;

        [SetUp]
        public async Task SetUp()
        {
            var dbContextOptions = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase($"AppDbContext_{Guid.NewGuid()}").Options;
            appDbContextMock = new Mock<AppDbContext>(dbContextOptions);

            mapper = new Mapper(new MapperConfiguration(c => c.AddProfile(new ProfileMapper())));

            httpClientFactoryMock = new Mock<IHttpClientFactory>();

            configurationMock = new Mock<IConfiguration>();
        }

        [Test]
        public async Task Create_Throws_InvalidDatabaseResultException()
        {
            // Arrange
            IEnumerable<Post> posts = [];
            var createPostDto = new Fixture().Build<CreatePostDto>().Create();

            var postDbSetMock = posts.BuildMock().BuildMockDbSet();

            appDbContextMock.Setup(x => x.Posts).Returns(postDbSetMock.Object);
            appDbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(0);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = () => postRepository.Create(createPostDto);

            // Assert
            await act.Should().ThrowAsync<InvalidDatabaseResultException>();
        }

        [Test]
        public async Task Create_Returns_NotNullObject()
        {
            // Arrange
            IEnumerable<Post> posts = [];
            var createPostDto = new Fixture().Build<CreatePostDto>().Create();

            var postDbSetMock = posts.BuildMock().BuildMockDbSet();

            appDbContextMock.Setup(x => x.Posts).Returns(postDbSetMock.Object);
            appDbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.Create(createPostDto);

            // Assert
            act.Should().BeAssignableTo<PostDto>().And.NotBeNull().And.BeEquivalentTo(createPostDto);
        }
    }
}