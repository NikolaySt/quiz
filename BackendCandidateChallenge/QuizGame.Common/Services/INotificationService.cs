namespace QuizGame.Common.Services
{
    public interface INotificationService
    {
        Task Publish<T>(T message);
    }
}