using System.Threading.Tasks;
using MassTransit;
using QuizGame.Common.Messages.Quiz;
using QuizGame.Common.Services;
using Xunit;

namespace QuizGame.Service.Tests;

public class NotificationServiceTest : SuperTest
{
    [Fact]
    public async Task Publish()
    {
        var harness = await GetMassTransitHarness(new QuizCreatedConsumer());
        var service = new NotificationService(harness.Bus);

        await service.Publish(new QuizCreatedMessage
        {
            QuizId = 1,
            Title = "123"
        });

        Assert.True(await harness.Consumed.Any<QuizCreatedMessage>());
    }

    /// <summary>
    /// Fake consumer, due to a missing service for consuming the published messages
    /// </summary>
    public class QuizCreatedConsumer : IConsumer<QuizCreatedMessage>
    {
        public async Task Consume(ConsumeContext<QuizCreatedMessage> context) => await Task.CompletedTask;
    }
}