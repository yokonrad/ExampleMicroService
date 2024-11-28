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

namespace PostMicroService.Tests.Repositories.PostCommands
{
    internal class GetAllCommandTest
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
        public async Task GetAll_Returns_EmptyCollection()
        {
            // Arrange
            IEnumerable<Post> posts = [];
            var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);

            var postDbSetMock = posts.BuildMock().BuildMockDbSet();

            appDbContextMock.Setup(x => x.Posts).Returns(postDbSetMock.Object);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.GetAll();

            // Assert
            act.Should().BeAssignableTo<IEnumerable<PostDto>>().And.BeEmpty().And.BeEquivalentTo(postsDto);
        }

        [Test]
        public async Task GetAll_Returns_NotEmptyCollection()
        {
            // Arrange
            IEnumerable<Post> posts = new Fixture().Build<Post>().CreateMany(10).ToArray();
            var postsDto = mapper.Map<IEnumerable<PostDto>>(posts);

            var postDbSetMock = posts.BuildMock().BuildMockDbSet();

            appDbContextMock.Setup(x => x.Posts).Returns(postDbSetMock.Object);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.GetAll();

            // Assert
            act.Should().BeAssignableTo<IEnumerable<PostDto>>().And.NotBeEmpty().And.BeEquivalentTo(postsDto);
        }
    }
}