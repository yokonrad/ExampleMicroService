using AutoFixture;
using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PostMicroService.Requests;
using Shared.Exceptions;
using Shared.Requests;
using Shared.Responds;

namespace PostMicroService.Tests.Requests.PostCommands
{
    internal class CreateCommandTest
    {
        private Mock<IRequestClient<DeletePostRequest>> deletePostRequestClientMock;

        [SetUp]
        public async Task SetUp()
        {
            deletePostRequestClientMock = new Mock<IRequestClient<DeletePostRequest>>();
        }

        [Test]
        public async Task Create_Throws_InvalidResponseException()
        {
            // Arrange
            var createPostRequest = new Fixture().Build<CreatePostRequest>().Create();
            var postNotCreatedRespond = new Fixture().Build<PostNotCreatedRespond>().With(x => x.CorrelationId, createPostRequest.CorrelationId).With(x => x.Id, createPostRequest.Id).Create();

            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(c => c.AddHandler<CreatePostRequest>(async c => await c.RespondAsync(postNotCreatedRespond)))
                .BuildServiceProvider(true);

            var testHarness = provider.GetRequiredService<ITestHarness>();

            await testHarness.Start();

            var createPostRequestClient = testHarness.GetRequestClient<CreatePostRequest>();

            var postRequest = new PostRequest(createPostRequestClient, deletePostRequestClientMock.Object);

            // Act
            var act = () => postRequest.Create(createPostRequest);

            // Assert
            await act.Should().ThrowAsync<InvalidResponseException>();
        }

        [Test]
        public async Task Create_Returns_NotNullObject()
        {
            // Arrange
            var createPostRequest = new Fixture().Build<CreatePostRequest>().Create();
            var postCreatedRespond = new Fixture().Build<PostCreatedRespond>().With(x => x.CorrelationId, createPostRequest.CorrelationId).With(x => x.Id, createPostRequest.Id).Create();

            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(c => c.AddHandler<CreatePostRequest>(async c => await c.RespondAsync(postCreatedRespond)))
                .BuildServiceProvider(true);

            var testHarness = provider.GetRequiredService<ITestHarness>();

            await testHarness.Start();

            var createPostRequestClient = testHarness.GetRequestClient<CreatePostRequest>();

            var postRequest = new PostRequest(createPostRequestClient, deletePostRequestClientMock.Object);

            // Act
            var act = await postRequest.Create(createPostRequest);

            // Assert
            act.Should().BeAssignableTo<PostCreatedRespond>().And.NotBeNull().And.BeEquivalentTo(postCreatedRespond);
        }
    }
}