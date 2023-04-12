using Application.Models.Broker;

namespace Infrastructure.Broker
{
    public interface IAccountDataProvider
    {
        event EventHandler<AccountOrderUpdate>? RaiseUpdateOrder;

        Task StartAccountDataProviderAsync();
        //Task StopAccountDataProviderAsync(string derivates);
    }
}