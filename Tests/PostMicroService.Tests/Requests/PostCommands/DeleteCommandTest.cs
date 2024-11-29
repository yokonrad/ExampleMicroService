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
    internal class DeleteCommandTest
    {
        private Mock<IRequestClient<CreatePostRequest>> createPostRequestClientMock;

        [SetUp]
        public async Task SetUp()
        {
            createPostRequestClientMock = new Mock<IRequestClient<CreatePostRequest>>();
        }

        [Test]
        public async Task Delete_Throws_InvalidResponseException()
        {
            // Arrange
            var deletePostRequest = new Fixture().Build<DeletePostRequest>().Create();
            var postNotDeletedRespond = new Fixture().Build<PostNotDeletedRespond>().With(x => x.CorrelationId, deletePostRequest.CorrelationId).With(x => x.Id, deletePostRequest.Id).Create();

            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(c => c.AddHandler<DeletePostRequest>(async c => await c.RespondAsync(postNotDeletedRespond)))
                .BuildServiceProvider(true);

            var testHarness = provider.GetRequiredService<ITestHarness>();

            await testHarness.Start();

            var deletePostRequestClient = testHarness.GetRequestClient<DeletePostRequest>();

            var postRequest = new PostRequest(createPostRequestClientMock.Object, deletePostRequestClient);

            // Act
            var act = () => postRequest.Delete(deletePostRequest);

            // Assert
            await act.Should().ThrowAsync<InvalidResponseException>();
        }

        [Test]
        public async Task Delete_Returns_NotNullObject()
        {
            // Arrange
            var deletePostRequest = new Fixture().Build<DeletePostRequest>().Create();
            var postDeletedRespond = new Fixture().Build<PostDeletedRespond>().With(x => x.CorrelationId, deletePostRequest.CorrelationId).With(x => x.Id, deletePostRequest.Id).Create();

            await using var provider = new ServiceCollection()
                .AddMassTransitTestHarness(c => c.AddHandler<DeletePostRequest>(async c => await c.RespondAsync(postDeletedRespond)))
                .BuildServiceProvider(true);

            var testHarness = provider.GetRequiredService<ITestHarness>();

            await testHarness.Start();

            var deletePostRequestClient = testHarness.GetRequestClient<DeletePostRequest>();

            var postRequest = new PostRequest(createPostRequestClientMock.Object, deletePostRequestClient);

            // Act
            var act = await postRequest.Delete(deletePostRequest);

            // Assert
            act.Should().BeAssignableTo<PostDeletedRespond>().And.NotBeNull().And.BeEquivalentTo(postDeletedRespond);
        }
    }
}