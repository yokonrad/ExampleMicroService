using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MockQueryable;
using MockQueryable.Moq;
using Moq;
using Newtonsoft.Json;
using PostMicroService.Data;
using PostMicroService.Dto;
using PostMicroService.Entities;
using PostMicroService.Helpers;
using PostMicroService.Repositories;
using RichardSzalay.MockHttp;
using Shared.Exceptions;
using System.Net;

namespace PostMicroService.Tests.Repositories.PostCommands
{
    internal class GetByIdCommentCommandTest
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
        public async Task GetByIdComment_Throws_NotFoundException()
        {
            // Arrange
            IEnumerable<Post> posts = [];
            var postDto = new Fixture().Build<PostDto>().Create();

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = () => postRepository.GetByIdComment(postDto.Id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task GetByIdComment_Throws_InvalidHttpResponseException()
        {
            // Arrange
            IEnumerable<Post> posts = new Fixture().Build<Post>().CreateMany(10).ToArray();
            var postDto = mapper.Map<PostDto>(posts.First());

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When($"http://localhost:5002/api/v1/comment/{postDto.Id}/post").Respond(HttpStatusCode.NotFound);

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient(mockHttpMessageHandler));
            configurationMock.SetupGet(x => x["Services:CommentService"]).Returns("http://localhost:5002");

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = () => postRepository.GetByIdComment(postDto.Id);

            // Assert
            await act.Should().ThrowAsync<InvalidHttpResponseException>();
        }

        [Test]
        public async Task GetByIdComment_Returns_NotNullObject_WithoutComments()
        {
            // Arrange
            IEnumerable<Post> posts = new Fixture().Build<Post>().CreateMany(10).ToArray();
            var postDto = mapper.Map<PostDto>(posts.First());
            var commentsDto = Array.Empty<CommentDto>();
            var postCommentDto = mapper.Map<PostCommentDto>((postDto, commentsDto));

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When($"http://localhost:5002/api/v1/comment/{postDto.Id}/post").Respond("application/json", JsonConvert.SerializeObject(commentsDto));

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient(mockHttpMessageHandler));
            configurationMock.SetupGet(x => x["Services:CommentService"]).Returns("http://localhost:5002");

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.GetByIdComment(postDto.Id);

            // Assert
            act.Should().BeAssignableTo<PostCommentDto>().And.NotBeNull().And.BeEquivalentTo(postCommentDto);
        }

        [Test]
        public async Task GetByIdComment_Returns_NotNullObject_WithComments()
        {
            // Arrange
            IEnumerable<Post> posts = new Fixture().Build<Post>().CreateMany(10).ToArray();
            var postDto = mapper.Map<PostDto>(posts.First());
            var commentsDto = new Fixture().Build<CommentDto>().CreateMany(10).ToArray();
            var postCommentDto = mapper.Map<PostCommentDto>((postDto, commentsDto));

            var postsDbSetMock = posts.BuildMock().BuildMockDbSet();

            postsDbSetMock.Setup(x => x.FindAsync(postDto.Id)).ReturnsAsync(posts.FirstOrDefault(x => x.Id == postDto.Id));
            appDbContextMock.Setup(x => x.Posts).Returns(postsDbSetMock.Object);

            var mockHttpMessageHandler = new MockHttpMessageHandler();
            mockHttpMessageHandler.When($"http://localhost:5002/api/v1/comment/{postDto.Id}/post").Respond("application/json", JsonConvert.SerializeObject(commentsDto));

            httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(new HttpClient(mockHttpMessageHandler));
            configurationMock.SetupGet(x => x["Services:CommentService"]).Returns("http://localhost:5002");

            var postRepository = new PostRepository(appDbContextMock.Object, mapper, httpClientFactoryMock.Object, configurationMock.Object);

            // Act
            var act = await postRepository.GetByIdComment(postDto.Id);

            // Assert
            act.Should().BeAssignableTo<PostCommentDto>().And.NotBeNull().And.BeEquivalentTo(postCommentDto);
        }
    }
}