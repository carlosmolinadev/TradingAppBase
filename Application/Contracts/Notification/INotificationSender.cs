namespace Application.Contracts.Communication
{
    public interface INotificationSender
    {
        Task<bool> SendMessage(string message);
    }
}