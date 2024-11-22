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
    internal class GetByIdCommandTest
    {
        private Mock<AppDbContext> appDbContextMock;
        private IMapper mapper;
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
        public async Task GetById_Throws_NotFoundException()
        {
            // Arrange
            IEnumerable<Post> posts = [];
            var postDto = new Fixture().Build<PostDto>().Create();

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = () => postRepository.GetById(postDto.Id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task GetById_Returns_NotNullObject()
        {
            // Arrange
            IEnumerable<Post> posts = [new Fixture().Build<Post>().Create()];
            var postDto = mapper.Map<PostDto>(posts.First());

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.GetById(postDto.Id);

            // Assert
            act.Should().BeOfType<PostDto>().And.NotBeNull().And.BeEquivalentTo(postDto);
        }
    }
}