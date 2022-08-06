using MassTransit;

namespace QuizGame.Common.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IBus _publisher;

        public NotificationService(IBus publisher)
        {
            _publisher = publisher;
        }

        public async Task Publish<T>(T message)
        {
            if (message == null) return;
            ThreadPool.QueueUserWorkItem(async (s) =>
            {
                await _publisher.Publish(message);
            });
            await Task.CompletedTask;
        }
    }
}